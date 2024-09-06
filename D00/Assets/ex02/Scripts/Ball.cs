using UnityEngine;

public class Ball : MonoBehaviour
{
    private static readonly float BORDER_SIZE_Y = 7.5f;
    private static readonly float HOLE_SIZE_Y = 0.4f;
    private static readonly float SPEED_INCREMENT_SIZE = 0.05f;
    private static readonly float BALL_SIZE_IN_HOLE = 0.1f;

    public float MaxSpeed = 50.0f;
    public float Friction = 0.005f;
    private float speed = 0.0f;
    private Vector2 direction = Vector2.down;

    [HideInInspector] public GameObject Hole;

    private int Score = -15;
    private bool move = false;
    private bool gameOver = false;

    private void Start()
    {
        Hole = GameObject.Find("Hole");
    }

    private void Update()
    {
        ToMove();
    }

    private void ToMove()
    {
        if (speed > 0.0f)
        {
            move = true;
        }
        else
        {
            CheckScore();
            move = false;
        }

        gameObject.transform.Translate(speed * Time.deltaTime * direction);
        speed = Mathf.Clamp(speed - Friction, 0.0f, MaxSpeed);
        CheckFrame();
        CheckHole();
    }

    private void CheckFrame()
    {
        if (gameObject.transform.position.y < -BORDER_SIZE_Y)
            direction = Vector2.up;
        if (gameObject.transform.position.y > BORDER_SIZE_Y)
            direction = Vector2.down;
    }

    private void CheckHole()
    {
        if (gameObject.transform.position.y <= Hole.transform.position.y + HOLE_SIZE_Y &&
            gameObject.transform.position.y >= Hole.transform.position.y - HOLE_SIZE_Y &&
            (speed < MaxSpeed / 10.0f))
        {
            speed = 0.0f;
            gameOver = true;
            if (gameObject.transform.localScale.x >= BALL_SIZE_IN_HOLE)
            {
                gameObject.transform.localScale -= new Vector3(SPEED_INCREMENT_SIZE, SPEED_INCREMENT_SIZE, 0.0f);
            }
        }
    }

    private void CheckScore()
    {
        if (move == true && gameOver != true)
        {
            Score += 5;
            Debug.Log("Score: " + Score);
        }
        else if (move == true && gameOver == true)
        {
            if (Score > 0)
            {
                Debug.Log("Score: " + Score + ". You loose");
            }
            else
            {
                Debug.Log("Score: " + Score + ". You win");
            }
        }
    }

    public float GetSpeed()
    {
        return (speed);
    }

    public void SetSpeed(float newSpeed)
    {
        if (gameOver != true)
            speed = newSpeed;
    }

    public void SetDirection(Vector2 newDirection)
    {
        if (gameOver != true)
            direction = newDirection;
    }

    public bool GetMove()
    {
        return (move);
    }
}
