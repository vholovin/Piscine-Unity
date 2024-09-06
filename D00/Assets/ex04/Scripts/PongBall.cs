using UnityEngine;

public class PongBall : MonoBehaviour
{
    private static readonly float BORDER_SIZE = 1.0f;

    private static readonly float BORDER_UP = 14.5f - BORDER_SIZE;
    private static readonly float BORDER_DOWN = -(14.5f - BORDER_SIZE);
    private static readonly float BORDER_LEFT = -(15.5f - BORDER_SIZE);
    private static readonly float BORDER_RIGHT = 15.5f - BORDER_SIZE;

    private static readonly float PLAYER_SIZE_X = 1.0f;
    private static readonly float PLAYER_SIZE_Y = 5.0f;

    private static readonly float PLAYER_LEFT = -(14.0f - PLAYER_SIZE_X);
    private static readonly float PLAYER_RIGHT = 14.0f - PLAYER_SIZE_X;

    private static readonly int RANDOM_DIRECTION_BALL = 5;

    public uint Speed = 10;
    private Vector2 direction;

    [HideInInspector] public GameObject Player1;
    [HideInInspector] public GameObject Player2;
    [HideInInspector] public GameObject PlayersControl;

    void Start()
    {
        Player1 = GameObject.Find("player1");
        Player2 = GameObject.Find("player2");
        PlayersControl = GameObject.Find("Players");
        ResetGame();
    }

    void Update()
    {
        ToMove();
    }

    private void ResetGame()
    {
        int x = 0;
        int y = 0;

        gameObject.transform.position = new Vector2(0.0f, 0.0f);

        while (x == 0)
        {
            x = Random.Range(-RANDOM_DIRECTION_BALL, RANDOM_DIRECTION_BALL);
        }
        while (y == 0)
        {
            y = Random.Range(-RANDOM_DIRECTION_BALL, RANDOM_DIRECTION_BALL);
        }

        direction = new Vector2(x, y);
        direction = direction.normalized;
    }

    private void ToMove()
    {
        gameObject.transform.Translate(Speed * Time.deltaTime * direction);
        CheckBoard();
    }

    private void CheckBoard()
    {
        if ((gameObject.transform.position.y > BORDER_UP && direction.y > 0f) ||
            (gameObject.transform.position.y < BORDER_DOWN && direction.y < 0f))
        {
            direction *= new Vector2(1, -1);
        }
        else if (gameObject.transform.position.x <= PLAYER_LEFT && direction.x < 0f &&
                 (Player1.transform.position.y + PLAYER_SIZE_Y / 2) >= gameObject.transform.position.y &&
                 (Player1.transform.position.y - PLAYER_SIZE_Y / 2) <= gameObject.transform.position.y)
        {
            direction *= new Vector2(-1, 1);
        }
        else if (gameObject.transform.position.x >= PLAYER_RIGHT && direction.x > 0f &&
                 (Player2.transform.position.y + PLAYER_SIZE_Y / 2) >= gameObject.transform.position.y &&
                 (Player2.transform.position.y - PLAYER_SIZE_Y / 2) <= gameObject.transform.position.y)
        {
            direction *= new Vector2(-1, 1);
        }
        else if (gameObject.transform.position.x <= BORDER_LEFT)
        {
            PlayersControl.GetComponent<Player>().AddScorePlayer2();
            DisplayScore();
            ResetGame();
        }
        else if (gameObject.transform.position.x >= BORDER_RIGHT)
        {
            PlayersControl.GetComponent<Player>().AddScorePlayer1();
            DisplayScore();
            ResetGame();
        }
    }

    private void DisplayScore()
    {
        Debug.Log("Player 1: " + PlayersControl.GetComponent<Player>().GetScorePlayer1() + " | Player 2: " + PlayersControl.GetComponent<Player>().GetScorePlayer2());
    }
}
