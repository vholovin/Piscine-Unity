using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
	public uint Speed = 5;
	public GameObject Head;
	public Sprite[] SpriteHeads;
	public GameObject Body;
	public Sprite[] SpriteBodies;
	public GameObject Legs;
	public AudioClip PickUp;
	public GameObject WeaponPos;
	private GameObject weapon = null;
	public AudioClip[] DeadSounds = new AudioClip[4];
	private bool dead = false;
	private Vector2 vector;


	private void Start()
	{
		if (Head != null && SpriteHeads.Length > 0)
		{
            Head.GetComponent<SpriteRenderer>().sprite = SpriteHeads[Random.Range(0, SpriteHeads.Length)];
        }
    }

	public void Move(Vector2 vectorMove)
	{
		if (vectorMove == Vector2.zero)
		{
            Legs.GetComponent<Animator>().Play("stay");
        }
        else
		{
            Legs.GetComponent<Animator>().Play("go");
        }

		gameObject.GetComponent<Rigidbody2D>().AddForce(vectorMove * Speed, ForceMode2D.Force);
	}

	public void Direct(Vector3 position)
	{
		vector = position - transform.position;
		vector.Normalize();

		float rot_z = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90f);
	}

	public void UpElement(GameObject newElement)
	{
		if (newElement.CompareTag("Weapon") && weapon == null)
		{
			gameObject.GetComponent<AudioSource>().clip = PickUp;
			gameObject.GetComponent<AudioSource>().Play();

			newElement.transform.SetParent(WeaponPos.transform);
			weapon = newElement;

			weapon.GetComponent<WeaponParameters>().SetUse(true);

			weapon.transform.SetPositionAndRotation(WeaponPos.transform.position, WeaponPos.transform.rotation);

		}
	}

	public void DownElement()
	{
		if (weapon != null)
		{
			weapon.GetComponent<WeaponParameters>().SetUse(false);
			weapon.transform.parent = null;
			weapon = null;
		}
	}

	public void ToShot()
	{
		if (weapon != null)
		{
			weapon.GetComponent<WeaponParameters>().Shot(vector);
		}
	}

	public string GetNumberShot()
	{
		if (weapon != null)
		{
			uint number = weapon.GetComponent<WeaponParameters>().CurNumberShot;

            if (number < 100)
			{
				return number.ToString();
			}
			else
			{
				return "";
			}
        }
		else
		{
            return "";
        }
	}

	private void ToDead()
	{
		gameObject.GetComponent<AudioSource>().clip = DeadSounds[Random.Range(0, DeadSounds.Length)];
		gameObject.GetComponent<AudioSource>().Play();
		gameObject.GetComponent<Rigidbody2D>().AddTorque(Speed / 5f, ForceMode2D.Impulse);
	}


	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Ammo"))
		{
    		if (dead == false)
    		{
    			dead = true;
				ToDead();
				Destroy(gameObject, 1f);
    		}
		}
	}
}
