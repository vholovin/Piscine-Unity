using UnityEngine;

public class Club : MonoBehaviour
{
    private static readonly float OFFSET_CLUB_X = -0.15f;
    private static readonly float OFFSET_CLUB_Y = 1.2f;

    private static readonly float DELTA_FORCE = 0.15f;

    private static readonly float DISTANCE_CLUB = 2f;

    [HideInInspector] public GameObject Ball;
    [HideInInspector] public GameObject Hole;
    private bool keySpace = false;

    private float force = 0.0f;
    public float MaxForce = 10.0f;

    void Start()
    {
        Ball = GameObject.Find("Ball");
        Hole = GameObject.Find("Hole");
    }

    void Update()
    {
        SetPositionClub();
        KeyDownSpace();
        KeyUpSpace();
    }

    private void SetPositionClub()
    {
        if (keySpace == false && Ball.GetComponent<Ball>().GetSpeed() == 0f)
        {
            if (Ball.transform.position.y > Hole.transform.position.y)
                transform.position = new Vector2(Ball.transform.position.x + OFFSET_CLUB_X, Ball.transform.position.y + OFFSET_CLUB_Y);
            else if (Ball.transform.position.y < Hole.transform.position.y)
                transform.position = new Vector2(Ball.transform.position.x + OFFSET_CLUB_X, Ball.transform.position.y);
        }
    }

    private void KeyDownSpace()
    {
        if (Input.GetKey(KeyCode.Space) && Ball.GetComponent<Ball>().GetSpeed() == 0f)
        {
            keySpace = true;
            if (force <= MaxForce)
            {
                force += DELTA_FORCE;
                if (Ball.transform.position.y > Hole.transform.position.y)
                    gameObject.transform.Translate(DISTANCE_CLUB * Time.deltaTime * Vector2.up);
                else if (Ball.transform.position.y < Hole.transform.position.y)
                    gameObject.transform.Translate(DISTANCE_CLUB * Time.deltaTime * Vector2.down);
            }
        }
    }

    private void KeyUpSpace()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Ball.transform.position.y > Hole.transform.position.y)
            {
                while (gameObject.transform.position.y > Ball.transform.position.y)
                    gameObject.transform.Translate(Vector2.down * Time.deltaTime);
            }
            else if (Ball.transform.position.y < Hole.transform.position.y)
            {
                while (gameObject.transform.position.y < Ball.transform.position.y)
                    gameObject.transform.Translate(Vector2.up * Time.deltaTime);
            }

            if (Ball.transform.position.y > Hole.transform.position.y)
            {
                Ball.GetComponent<Ball>().SetDirection(Vector2.down);
            }
            else if (Ball.transform.position.y < Hole.transform.position.y)
            {
                Ball.GetComponent<Ball>().SetDirection(Vector2.up);
            }

            Ball.GetComponent<Ball>().SetSpeed(force);
            keySpace = false;
            force = 0.0f;
        }
    }
}
