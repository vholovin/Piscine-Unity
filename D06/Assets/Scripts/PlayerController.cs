using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeedNormal = 2.0f;
    public float MoveSpeedShift = 5.0f;
    public float RotateSpeed = 150.0f;
    private enum SoundPlayerStepsEnum { stay, normal, speed };
    private SoundPlayerStepsEnum PlayerSpeed = SoundPlayerStepsEnum.stay;

    public GameObject SoundSteps;
    public bool PlayingSoundSteps = true;
    public float WaitSoundStepsNormal = 0.5f;
    public float WaitSoundStepsShift = 0.2f;

    public bool LightDetected = false;
    public bool CameraDetected = false;

    private bool isKeyE = false;
    public bool Card = false;
    public bool GunInfo = false;
    public bool MapInfo = false;

    public GameObject Messenge;

    private void Start()
    {
        StartCoroutine(nameof(PlaySoundSteps));
    }

    private void Update()
    {
        ToMove();
        ToRotate();
        ToPressE();
    }


    IEnumerator PlaySoundSteps()
    {
        while (PlayingSoundSteps)
        {
            float wait = 0.0f;

            if (PlayerSpeed == SoundPlayerStepsEnum.normal)
            {
                SoundSteps.GetComponent<AudioSource>().Play();
                wait = WaitSoundStepsNormal;
            }
            else if (PlayerSpeed == SoundPlayerStepsEnum.speed)
            {
                SoundSteps.GetComponent<AudioSource>().Play();
                wait = WaitSoundStepsShift;
            }
            else
            {
                SoundSteps.GetComponent<AudioSource>().Stop();
            }

            yield return new WaitForSeconds(wait);
        }
    }

    private void ToMove()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            PlayerSpeed = SoundPlayerStepsEnum.stay;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                PlayerSpeed = SoundPlayerStepsEnum.speed;
                move *= MoveSpeedShift;
            }
            else
            {
                PlayerSpeed = SoundPlayerStepsEnum.normal;
                move *= MoveSpeedNormal;
            }
        }

        gameObject.transform.Translate(move * Time.deltaTime);
    }

    private void ToRotate()
    {
        float y = Camera.main.gameObject.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * RotateSpeed * Time.deltaTime;
        float x = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, x, 0);
        Camera.main.gameObject.transform.rotation = Quaternion.Euler(y, x, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LightDetect"))
        {
            LightDetected = true;
        }

        if (other.gameObject.CompareTag("CameraDetect"))
        {
            CameraDetected = true;
        }

        if (other.CompareTag("Key") || other.CompareTag("Fan") || other.CompareTag("Switch") || other.CompareTag("Info"))
        {
            Messenge.SetActive(true);
            Messenge.GetComponent<Text>().text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            ToGetKey(other);
        }


        if (other.CompareTag("Fan"))
        {
            ToGetFan(other);
        }

        if (other.CompareTag("Switch"))
        {
            ToGetSwitch(other);
        }

        if (other.CompareTag("Info"))
        {
            ToGetInfo(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LightDetect"))
        {
            LightDetected = false;
        }

        if (other.gameObject.CompareTag("CameraDetect"))
        {
            CameraDetected = false;
        }

        Messenge.SetActive(false);
    }

    private void ToPressE()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
        {
            isKeyE = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            isKeyE = false;
        }

    }

    private void ToGetKey(Collider key)
    {
        Messenge.GetComponent<Text>().text = "press 'E' to take key";
        Debug.Log(Messenge);

        if (isKeyE == true)
        {
            Card = true;
            Destroy(key.gameObject);
            Messenge.SetActive(false);
        }
    }

    private void ToGetFan(Collider fan)
    {
        Messenge.GetComponent<Text>().text = "press 'E' to broke fan";
        Debug.Log(Messenge);

        if (fan.gameObject.GetComponent<ActiveElement>() && isKeyE == true)
        {
            fan.gameObject.GetComponent<ActiveElement>().ToActivate();
        }
    }

    private void ToGetSwitch(Collider switchDoor)
    {
        Messenge.GetComponent<Text>().text = "press 'E' to switch door";
        Debug.Log(Messenge);

        if (switchDoor.gameObject.GetComponent<ActiveElement>() && isKeyE == true)
        {
            switchDoor.gameObject.GetComponent<ActiveElement>().ToActivate(Card);
        }
    }

    private void ToGetInfo(Collider info)
    {
        Messenge.GetComponent<Text>().text = "press 'E' to take info";
        Debug.Log(Messenge);

        if (isKeyE == true)
        {
            if (info.gameObject.name == "GunInfo")
            {
                GunInfo = true;
            }
 
            if (info.gameObject.name == "MapInfo")
            {
                MapInfo = true;
            }

            Destroy(info.gameObject);
            Messenge.SetActive(false);
        }
    }
}

