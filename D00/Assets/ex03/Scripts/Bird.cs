using UnityEngine;

public class Bird : MonoBehaviour
{
    public float UpInpulse = 2.5f;

    private uint Score = 0;
    private uint ScoreDecrement = 5;

    private bool isAlive = true;
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = gameObject.AddComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAlive)
        {
            UpdateVerticalPosition();
        }
        else
        {
            Debug.Log("Score: " + Score + "\nTime: " + Mathf.RoundToInt(Time.time) + "s");
            Destroy(gameObject);
        }
    }

    private void UpdateVerticalPosition()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(transform.up * UpInpulse, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "border" && isAlive != false)
        {
            isAlive = false;
        }
    }

    public bool GetAlive()
    {
        return isAlive;
    }

    public void AddScore()
    {
        Score += ScoreDecrement;
    }
}