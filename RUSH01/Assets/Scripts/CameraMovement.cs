using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;

    public uint DistanceMin = 2;
    public uint DistanceMax = 20;
    private float currentDistance;

    public uint SensitivityScroll = 40;

    public uint SensitivityX = 40;
    public uint SensitivityY = 120;
    private Vector2 angles;

    public int MinLimitY = 10;
    public int MaxLimitY = 80;
    
    private void Start ()
    {
        currentDistance = (DistanceMax + DistanceMin) / 2;

        Vector3 euler = transform.eulerAngles;
        angles.x = euler.y;
        angles.y = euler.x;

        SetRotation();
    }

    private void LateUpdate ()
    {
        if (Player)
        {
            RotationMouse();
            ScrollMouse();
            SetPosition();
        }
    }

    private void RotationMouse()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cursor.visible = false;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            SetRotation();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Cursor.visible = true;
        }
    }

    private void SetRotation()
    {
        angles.x += Input.GetAxis("Mouse X") * SensitivityX * currentDistance * Time.deltaTime;

        angles.y -= Input.GetAxis("Mouse Y") * SensitivityY * Time.deltaTime;
        angles.y = ClampAngle(angles.y, MinLimitY, MaxLimitY);

        transform.rotation = Quaternion.Euler(angles.y, angles.x, 0);
    }

    private void ScrollMouse()
    {
        currentDistance = Mathf.Clamp(currentDistance + Input.GetAxis("Mouse ScrollWheel") * SensitivityScroll * Time.deltaTime, DistanceMin, DistanceMax);
    }

    private static float ClampAngle(float angle, float min, float max) 
    {

        if (angle < -360)
        {
            angle += 360;
        }
        else if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }

    private void SetPosition()
    {
        transform.position = Player.position + transform.rotation * new Vector3(0.0f, 0.0f, - currentDistance);
    }
}