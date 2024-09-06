using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly float BORDER_SIZE = 0.5f;

    private static readonly float BORDER_UP = 14.5f - BORDER_SIZE;
    private static readonly float BORDER_DOWN = -(14.5f - BORDER_SIZE);

    private static readonly float PLAYER_SIZE_Y = 5.0f;

    public uint Speed = 20;
    private uint scorePlayer1 = 0;
    private uint scorePlayer2 = 0;
    [HideInInspector] public GameObject Player1;
    [HideInInspector] public GameObject Player2;

    void Start()
    {
        Player1 = GameObject.Find("player1");
        Player2 = GameObject.Find("player2");
    }

    void Update()
    {
        KeyPlayer1();
        KeyPlayer2();
    }

    private void KeyPlayer1()
    {
        if (Input.GetKey(KeyCode.W) && Player1.transform.position.y < (BORDER_UP - PLAYER_SIZE_Y / 2))
        {
            Player1.transform.Translate(Speed * Time.deltaTime * Vector2.up);
        }
        if (Input.GetKey(KeyCode.S) && Player1.transform.position.y > (BORDER_DOWN + PLAYER_SIZE_Y / 2))
        {
            Player1.transform.Translate(Speed * Time.deltaTime * Vector2.down);
        }
    }

    private void KeyPlayer2()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Player2.transform.position.y < (BORDER_UP - PLAYER_SIZE_Y / 2))
        {
            Player2.transform.Translate(Speed * Time.deltaTime * Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Player2.transform.position.y > (BORDER_DOWN + PLAYER_SIZE_Y / 2))
        {
            Player2.transform.Translate(Speed * Time.deltaTime * Vector3.down);
        }
    }

    public void AddScorePlayer1()
    {
        scorePlayer1++;
    }

    public void AddScorePlayer2()
    {
        scorePlayer2++;
    }

    public uint GetScorePlayer1()
    {
        return (scorePlayer1);
    }

    public uint GetScorePlayer2()
    {
        return (scorePlayer2);
    }
}
