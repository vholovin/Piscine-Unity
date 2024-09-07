using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerAI : MonoBehaviour
{
	private CharacterMove Character;

	private void Awake()
	{
		Character = gameObject.GetComponent<CharacterMove>();
	}
}
