using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex00 : MonoBehaviour
{
    public float Speed = 1.0f;
    public float Jump = 1.0f;

    private Vector2 movement;

    public GameObject[] Players;
    private uint iter = 0;

    private void Update()
    {
        ChangeIter();
        MoveKeys();
        ResetScene();
        SetCameraPosition();
    }

    private void FixedUpdate()
    {
        Move(movement);
    }

    private void ChangeIter()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            iter = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            iter = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            iter = 2;
        }
    }

    private void MoveKeys()
    {
        movement = new Vector2(Input.GetAxis("Horizontal") * Speed, 0);
        movement += Vector2.up * (Input.GetKey(KeyCode.Space) ? Jump : 0);
    }

    private void ResetScene()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("ex00", LoadSceneMode.Single);
        }
    }

    private void SetCameraPosition()
    {
        Camera.main.transform.position = new Vector3(Players[iter].transform.position.x, Players[iter].transform.position.y, -10.0f);
    }

    private void Move(Vector2 direction)
    {
        Players[iter].GetComponent<Rigidbody2D>().AddForce(direction);
    }
}
