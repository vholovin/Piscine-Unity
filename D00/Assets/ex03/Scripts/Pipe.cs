using UnityEngine;

public class Pipe : MonoBehaviour
{
    public static readonly float BORDER_LEFT = -10.0f;
    public static readonly float BORDER_RIGHT = 10.0f;

    public float SpeedAccelerator = 0.5f;
    public static float SpeedFly = 1.0f;
    private bool canUpdateSpeed = false;

    public float RandomRangePosY = 2.0f;

    [HideInInspector] public GameObject Bird;

    private void Start()
    {
        Bird = GameObject.Find("Bird");
    }

    private void Update()
    {
        if (Bird != null)
        {
            if (Bird.GetComponent<Bird>().GetAlive())
            {
                Move();
                UpdateBorderPosition();
                UpdateSpeed();
            }
        }
    }

    private void Move()
    {
        gameObject.transform.Translate(SpeedFly * Time.deltaTime * Vector2.left);
    }

    private void UpdateBorderPosition()
    {
        if (gameObject.transform.position.x < BORDER_LEFT)
        {
            gameObject.transform.position = new Vector2(BORDER_RIGHT, Random.Range(-RandomRangePosY, RandomRangePosY));
        }
    }

    private void UpdateSpeed()
    {
        if (gameObject.transform.position.x >= Bird.transform.position.x)
        {
            canUpdateSpeed = true;
        }

        if (gameObject.transform.position.x <= Bird.transform.position.x && canUpdateSpeed == true)
        {
            canUpdateSpeed = false;
            SpeedFly += SpeedAccelerator;
            Bird.GetComponent<Bird>().AddScore();
        }
    }
}