using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    private Vector3 offset;

    public float ZoomSpeed = 10f;
    public float RotateSpeed = 100f;

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector3 resetPos;
    private Quaternion resetRot;

    private void Start()
    {
        SetOffset();

        resetPos = transform.position - Target.transform.position;
        resetRot = transform.rotation;
    }

    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = Target.transform.position + offset;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            x = Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime;
            transform.RotateAround(Target.transform.position, Vector3.up, x);

            y = Input.GetAxis("Mouse Y") * ZoomSpeed * Time.deltaTime;
            transform.Translate(0, 0, y);

            SetOffset();
        }
        else if (Input.GetKey(KeyCode.R))
        {

            transform.SetPositionAndRotation(Target.transform.position + resetPos, resetRot);

            SetOffset();
        }
    }
    private void SetOffset()
    {
        offset = transform.position - Target.transform.position;
    }
}
