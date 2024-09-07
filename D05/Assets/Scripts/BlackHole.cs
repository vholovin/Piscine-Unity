using UnityEngine;

public class BlackHole : MonoBehaviour
{
	private bool inHole = false;

	public bool GetInHole()
	{
		return inHole;
	}


	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Golf"))
		{
			collision.gameObject.SetActive(false);

            gameObject.GetComponent<AudioSource>().Play();

			inHole = true;
		}
	}
}
