using UnityEngine;
using UnityEngine.UI;

public class CameraPlayer : MonoBehaviour
{
    public uint SpeedMoveCamera = 10;
    public uint SpeedRotateCamera = 100;

    public GameObject Golf;

    public float DistanceMin = 1.0f;
    public float DistanceMax = 2.0f;
    private float distance;

    public GameObject ForceSlider;
    private bool upForceSlider = false;
    public uint MinForce = 0;
    public uint MaxForce = 100;
    public uint SteepForce = 2;
    private bool keyDownSpace1 = false;
    private bool keyDownSpace2 = false;

    public Vector3 CompensationPosition = new(0f, 0.42f, 0.0f);

    public GameObject Arrow;
    private float timer = 0f;

    private void Start()
    {
        ForceSlider.SetActive(false);
        ForceSlider.GetComponent<Slider>().minValue = MinForce;
        ForceSlider.GetComponent<Slider>().maxValue = MaxForce;

        Arrow.SetActive(false);

        distance = (DistanceMax + DistanceMin) / 2.0f;
    }

    private void Update()
    {
        UpdateTimer();

        MoveCamera();
        RotateCamera();

        SetPosition();

        SetSpace();
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(SpeedMoveCamera * Time.deltaTime * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(SpeedMoveCamera * Time.deltaTime * Vector3.back);
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(SpeedMoveCamera * Time.deltaTime * Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(SpeedMoveCamera * Time.deltaTime * Vector3.left);

        }

        if (Input.GetKey(KeyCode.E))
        {
            gameObject.transform.Translate(SpeedMoveCamera * Time.deltaTime * Vector3.up);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            gameObject.transform.Translate(SpeedMoveCamera * Time.deltaTime * Vector3.down);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            keyDownSpace1 = false;
            keyDownSpace2 = false;
        }
    }

    private void RotateCamera()
    {
        float y = transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * SpeedRotateCamera * Time.deltaTime;
        float x = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * SpeedRotateCamera * Time.deltaTime;

        transform.rotation = Quaternion.Euler(y, x, 0);
    }

    private void SetPosition()
    {
        if (keyDownSpace1)
        {
//          gameObject.GetComponent<SphereCollider>().enabled = false;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), DistanceMin, DistanceMax);
            gameObject.transform.position = Golf.transform.position;

            if (Physics.Linecast(Golf.transform.position, transform.position, out RaycastHit hit))
            {
                distance -= hit.distance;
            }

            Vector3 newDistance = new(0.0f, 0.0f, -distance);
            gameObject.transform.position = transform.rotation * newDistance + Golf.transform.position + CompensationPosition;
        }
        else if (!keyDownSpace1)
        {
  //          gameObject.GetComponent<SphereCollider>().enabled = true;
        }

    }

    private void SetSpace()
    {
        if (Golf.activeSelf == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (keyDownSpace1 == false)
            {
                keyDownSpace1 = true;
                timer = 0;
            }
            else if (keyDownSpace1 == true && keyDownSpace2 == false && timer > 1.0f && Golf.GetComponent<Rigidbody>().velocity.magnitude == 0.0f)
            {
                keyDownSpace2 = true;
                ForceSlider.SetActive(true);
                Arrow.SetActive(true);
                Arrow.transform.position = Golf.transform.position;
            }
        }

        if (keyDownSpace1 && keyDownSpace2)
        {
            Arrow.transform.position = Golf.transform.position;

            if (upForceSlider)
            {
                ForceSlider.GetComponent<Slider>().value += SteepForce;
            }
            else
            {
                ForceSlider.GetComponent<Slider>().value -= SteepForce;
            }

            if (ForceSlider.GetComponent<Slider>().value >= ForceSlider.GetComponent<Slider>().maxValue)
            {
                upForceSlider = false;
            }
            else if (ForceSlider.GetComponent<Slider>().value <= ForceSlider.GetComponent<Slider>().minValue)
            {
                upForceSlider = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && keyDownSpace2)
        {
            keyDownSpace1 = false;
            keyDownSpace2 = false;

            ForceSlider.SetActive(false);
            Arrow.SetActive(false);

            Vector3 direction = Golf.transform.position - transform.position;
            Golf.GetComponent<Rigidbody>().AddForceAtPosition(
                                                                direction.normalized * ForceSlider.GetComponent<Slider>().value,
                                                                transform.position - CompensationPosition);

            GameObject.Find("GameManager").GetComponent<GolfManadger>().AddShotAndScore();
        }
    }
}
