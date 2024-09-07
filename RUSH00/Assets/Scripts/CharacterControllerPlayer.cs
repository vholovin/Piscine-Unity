using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControllerPlayer : MonoBehaviour
{
	private CharacterMove Character;

	public Text TextNumberShot;
	public GameObject finish;

	private bool finFlag = false;

    private void Awake()
	{
		Character = gameObject.GetComponent<CharacterMove>();
	}

	private void Update()
	{
		MoveCharacter();
		DirectCharacter();
		ActionKey();
		MouseKey();
		UpdateNumberShot();
	}

    private void MoveCharacter()
    {
        Vector2 vectorMove = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            vectorMove += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vectorMove -= Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            vectorMove += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
		{
            vectorMove += Vector2.right;
        }

        Character.Move(vectorMove);
    }

    private void DirectCharacter()
    {
        Character.Direct(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void ActionKey()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Collider2D[] UpElements = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 0.1f);
            foreach (Collider2D element in UpElements)
            {
                Character.UpElement(element.gameObject);
            }
        }
    }

    public void MouseKey()
    {
        if (Input.GetButton("Fire1"))
        {
            Character.ToShot();
        }
        if (Input.GetButton("Fire2"))
        {
            Character.DownElement();
        }
    }

    private void UpdateNumberShot()
	{
		if (TextNumberShot != null)
		{
            TextNumberShot.text = Character.GetComponent<CharacterMove>().GetNumberShot();
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("fin"))
		{
            finFlag = true;
        }
	}

    public bool GetFinish()
    {
        return finFlag;
    }

    public Vector3 GetCharacterPosition()
    {
        return Character.transform.position;
    }
}