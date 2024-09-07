using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParameters : MonoBehaviour
{
	public bool Use = false;
	public uint ImpulseFly = 10;
	public uint CurNumberShot = 0;
	public Sprite[] SpriteFullGun = new Sprite[12];
	public Sprite[] SpriteBodyGun = new Sprite[12];
	public Sprite[] SpriteShot = new Sprite[12];
	public float[] TimeAction = new float[12];
	public uint[] NumberShot = new uint[12];
	public AudioClip[] ShotSounds = new AudioClip[12];

	private int iter = 0;
	private float timer = 0f;
	private float timeDelay = 0.2f;
	private bool shot = true;

	private void Start()
	{
		iter = Random.Range(0,12);
		CurNumberShot = NumberShot[iter];
		gameObject.GetComponent<AudioSource>().clip = ShotSounds[iter];
		if (Use == true)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = SpriteBodyGun[iter];
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		}
		else
		{ 
			gameObject.GetComponent<SpriteRenderer>().sprite = SpriteFullGun[iter];
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			gameObject.GetComponent<Rigidbody2D>().drag = 5f;
			gameObject.GetComponent<Rigidbody2D>().angularDrag = 5f;
		}
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer > timeDelay && CurNumberShot > 0)
		{
			timer = 0;
			shot = true;
		}
	}

	public void SetUse(bool status)
	{
		if (status == true)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = SpriteBodyGun[iter];
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		}
		else if (status == false)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = SpriteFullGun[iter];
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			gameObject.GetComponent<Rigidbody2D>().drag = 5f;
			gameObject.GetComponent<Rigidbody2D>().angularDrag = 5f;
			gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * ImpulseFly, ForceMode2D.Impulse);
			gameObject.GetComponent<Rigidbody2D>().AddTorque(ImpulseFly, ForceMode2D.Impulse);
		}
	}

	public void Shot(Vector2 vector)
	{
		if (shot == true)
		{
			CurNumberShot--;
			shot = false;
			gameObject.GetComponent<AudioSource>().Play();

			GameObject ShotElement = new GameObject();
			ShotElement.name = "Ammo";
			ShotElement.tag = "Ammo";
			ShotElement.layer = LayerMask.NameToLayer("Ammo");
			ShotElement.transform.position = gameObject.transform.position;
			ShotElement.transform.rotation = gameObject.transform.rotation;
			ShotElement.transform.RotateAround(ShotElement.transform.position, Vector3.forward, -90);
			ShotElement.AddComponent<SpriteRenderer>().sprite = SpriteShot[iter];
			ShotElement.GetComponent<SpriteRenderer>().sortingLayerName = "hero";
			ShotElement.GetComponent<SpriteRenderer>().sortingOrder = 2;
			ShotElement.AddComponent<CircleCollider2D>();
			ShotElement.AddComponent<Rigidbody2D>().gravityScale = 0.0f;
			ShotElement.GetComponent<Rigidbody2D>().AddForce(vector * ImpulseFly, ForceMode2D.Impulse);
			ShotElement.AddComponent<WeaponAmmo>();
			Destroy(ShotElement, TimeAction[iter]);
		}
	}
}
