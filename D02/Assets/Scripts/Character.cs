using UnityEngine;

public class Character : MonoBehaviour
{
    private enum TypeDirection
    {
        UP,
        UP_LEFT,
        UP_RIGHT,
        LEFT,
        RIGHT,
        DOWN_LEFT,
        DOWN_RIGHT,
        DOWN,
        STAY
    };

    private TypeDirection direction;
    private Vector2 newPosition;

    public uint Speed = 1;

    public AudioClip[] SelectedSounds;

    public GameObject SelectObject;
    private bool isSelect = false;

    public float DirectionLimit = 0.1f;

    private void Start()
    {
        if (SelectObject)
        {
            SelectObject.SetActive(isSelect);
        }

        newPosition = gameObject.transform.position;
        direction = TypeDirection.STAY;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 curPosition = transform.position;

        if (direction != TypeDirection.STAY)
        {
            gameObject.transform.position = Vector2.MoveTowards(curPosition, newPosition, Speed * Time.deltaTime);
        }

        if (curPosition == newPosition && direction != TypeDirection.STAY)
        {
            gameObject.GetComponent<Animator>().Play("Stay");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.STAY;
        }
    }

    public void SetPosition(Vector2 position)
    {
        if (isSelect)
        {
            gameObject.GetComponent<AudioSource>().clip = SelectedSounds[Random.Range(0, SelectedSounds.Length)];
            gameObject.GetComponent<AudioSource>().Play();
            newPosition = position;
            SetDirection();
        }
    }

    private void SetDirection()
    {
        Vector2 newDirection = new(newPosition.x - transform.position.x, newPosition.y - transform.position.y);

        if (newDirection.x < 0f && newDirection.y >= -DirectionLimit && newDirection.y <= DirectionLimit)
        {
            gameObject.GetComponent<Animator>().Play("GoLeft");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.LEFT;
        }
        else if (newDirection.x > 0f && newDirection.y >= -DirectionLimit && newDirection.y <= DirectionLimit)
        {
            gameObject.GetComponent<Animator>().Play("GoRight");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.RIGHT;
        }
        else if (newDirection.x >= -DirectionLimit && newDirection.x <= DirectionLimit && newDirection.y > 0f)
        {
            gameObject.GetComponent<Animator>().Play("GoUp");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.UP;
        }
        else if (newDirection.x >= -DirectionLimit && newDirection.x <= DirectionLimit && newDirection.y < 0f)
        {
            gameObject.GetComponent<Animator>().Play("GoDown");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.DOWN;
        }
        else if (newDirection.x < 0f && newDirection.y > 0f)
        {
            gameObject.GetComponent<Animator>().Play("GoUpLeft");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.UP_LEFT;
        }
        else if (newDirection.x > 0f && newDirection.y > 0f)
        {
            gameObject.GetComponent<Animator>().Play("GoUpRight");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.UP_RIGHT;
        }
        else if (newDirection.x < 0f && newDirection.y < 0f)
        {
            gameObject.GetComponent<Animator>().Play("GoDownLeft");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.DOWN_LEFT;
        }
        else if (newDirection.x > 0f && newDirection.y < 0f)
        {
            gameObject.GetComponent<Animator>().Play("GoDownRight");
            gameObject.GetComponent<Animator>().speed = Speed;
            direction = TypeDirection.DOWN_RIGHT;
        }
    }

    public bool GetSelect()
    {
        return isSelect;
    }

    public void SetSelect(bool newSelect)
    {
        isSelect = newSelect;

        if (isSelect)
        {
            gameObject.GetComponent<AudioSource>().clip = SelectedSounds[Random.Range(0, SelectedSounds.Length)];
            gameObject.GetComponent<AudioSource>().Play();
        }

        if (SelectObject)
        {
            SelectObject.SetActive(isSelect);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider)
        {
            newPosition = transform.position;
        }
    }
}
