using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex01 : MonoBehaviour
{
    public GameObject[] Players;

    public float[] Speed = { 3.0f, 4.0f, 2.0f };
    public float[] Jump = { 30.0f, 40.0f, 25.0f };

    private static bool[] isJump = { true, true, true };
    private static bool[] isExit = { false, false, false };

    private Vector2 movement;

    private uint iter = 0;

    public int CurLevel = 0;
    public int MaxLevel = 3;

    [System.Obsolete]
    private void Start()
    {
        CurLevel = Application.loadedLevel;
    }

    void Update()
    {
        ChangeIter();
        MoveKeys();
        ResetScene();
        SetCameraPosition();
        CheckExit();
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
            isJump[iter] = true;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            iter = 1;
            isJump[iter] = true;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            iter = 2;
            isJump[iter] = true;
        }
    }

    private void MoveKeys()
    {
        movement = new Vector2(Input.GetAxis("Horizontal") * Speed[iter], 0);

        if (isJump[iter] == true)
        {
            movement += Vector2.up * (Input.GetKey(KeyCode.Space) ? Jump[iter] : 0);
        }
    }

    private void Move(Vector2 direction)
    {
        Players[iter].GetComponent<Rigidbody2D>().AddForce(direction);
    }

    private void ResetScene()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(CurLevel);
            for (int i = 0; i < MaxLevel; i++)
                isExit[i] = false;
        }
    }

    private void SetCameraPosition()
    {
        Camera.main.transform.position = new Vector3(Players[iter].transform.position.x, Players[iter].transform.position.y, -10.0f);
    }

    private void CheckExit()
    {
        if (isExit[0] == true && isExit[1] == true && isExit[2] == true)
        {
            for (int i = 0; i < MaxLevel; i++)
            {
                isExit[i] = false;
            }
            Debug.Log("WIN");
            LoadLevel();
        }
    }

    void LoadLevel()
    {
        CurLevel++;
        if (CurLevel > SceneManager.sceneCount + 1)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(CurLevel);
    }

    private void OnCollisionStay(Collision Collision)
    {
        if (!Collision.collider)
        {
            isJump[iter] = false;
        }
        else
        {
            isJump[iter] = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D Collision)
    {
        Vector2 normal = Collision.contacts[0].normal;
        float angle = Vector2.Angle(normal, Vector2.up);

        if (Mathf.Approximately(angle, 0))
        {
            isJump[iter] = true;
        }
        
        gameObject.transform.SetParent(Collision.gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D Collision)
    {
        isJump[iter] = false;
     //   gameObject.transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (iter == 0 && Collider.gameObject.transform.tag == "RedExit")
            isExit[iter] = true;
        if (iter == 1 && Collider.gameObject.transform.tag == "YellowExit")
            isExit[iter] = true;
        if (iter == 2 && Collider.gameObject.transform.tag == "BlueExit")
            isExit[iter] = true;
    }

    void OnTriggerExit2D(Collider2D Collider)
    {
        if (iter == 0 && Collider.gameObject.transform.tag == "RedExit")
            isExit[iter] = false;
        if (iter == 1 && Collider.gameObject.transform.tag == "YellowExit")
            isExit[iter] = false;
        if (iter == 2 && Collider.gameObject.transform.tag == "BlueExit")
            isExit[iter] = false;
    }
}