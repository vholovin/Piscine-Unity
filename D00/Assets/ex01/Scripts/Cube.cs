using UnityEngine;

public class Cube : MonoBehaviour
{
    private readonly uint MIN_SPEED = 2;
    private readonly uint MAX_SPEED = 6;

    private readonly float POSITION_DOWN = -2.5f;
    private readonly float POSITION_UP = 3.5f;

    private float speed;

    private void Start()
    {
        speed = Random.Range(MIN_SPEED, MAX_SPEED);
    }

    private void Update()
    {
        float precision = transform.position.y + Mathf.Abs(POSITION_DOWN);

        gameObject.transform.Translate(speed * Vector3.down * Time.deltaTime);

        if (transform.position.y < POSITION_DOWN)
        {
            Destroy(transform.gameObject);
            if (CompareTag("a_key"))
                CubeSpawner.ObjectCount[0] -= 1;
            else if (CompareTag("s_key"))
                CubeSpawner.ObjectCount[1] -= 1;
            else if (CompareTag("d_key"))
                CubeSpawner.ObjectCount[2] -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.A) && CompareTag("a_key")
            && transform.position.y > POSITION_DOWN && transform.position.y < POSITION_UP)
        {
            Debug.Log("Precision A: " + precision);
            Destroy(transform.gameObject);
            CubeSpawner.ObjectCount[0]--;
        }
        else if (Input.GetKeyDown(KeyCode.S) && CompareTag("s_key")
            && transform.position.y > POSITION_DOWN && transform.position.y < POSITION_UP)
        {
            Debug.Log("Precision S: " + precision);
            Destroy(transform.gameObject);
            CubeSpawner.ObjectCount[1]--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && CompareTag("d_key")
            && transform.position.y > POSITION_DOWN && transform.position.y < POSITION_UP)
        {
            Debug.Log("Precision D: " + precision);
            Destroy(transform.gameObject);
            CubeSpawner.ObjectCount[2]--;
        }
    }
}
