using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public bool UpDown = true;
    public uint AmplitudeUpDown = 10;

    public bool LeftRight = true;
    public uint AmplitudeLeftRight = 10;

    private Vector2 direction = Vector2.zero;
    private Vector2 startPosition;

    private void Start()
    {
        startPosition = gameObject.transform.position;

        if (UpDown && LeftRight)
        {
            direction = Vector2.up + Vector2.right;
        }
        else if (UpDown)
        {
            direction = Vector2.down;
        }
        else if (LeftRight)
        {
            direction = Vector2.left;
        }
    }

    private void Update()
    {
        SetDirection();
        Move();
    }

    private void SetDirection()
    {
        VerticalDirection();
        HorizontalDirection();
        NormalizedDirection();
    }

    private void VerticalDirection()
    {
        if (gameObject.transform.position.y > (startPosition.y + AmplitudeUpDown / 2.0f))
        {
            direction += Vector2.down;
        }
        else if (gameObject.transform.position.y < (startPosition.y - AmplitudeUpDown / 2.0f))
        {
            direction += Vector2.up;
        }
    }

    private void HorizontalDirection()
    {
        if (gameObject.transform.position.x > (startPosition.x + AmplitudeLeftRight / 2.0f))
        {
            direction += Vector2.left;
        }
        else if (gameObject.transform.position.x < (startPosition.x - AmplitudeLeftRight / 2.0f))
        {
            direction += Vector2.right;
        }
    }

    private void NormalizedDirection()
    {
        direction = direction.normalized;
    }

    private void Move()
    {
        gameObject.transform.Translate(direction * Time.deltaTime);
    }
}