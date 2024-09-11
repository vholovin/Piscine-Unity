using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] EnemyType = new GameObject[2];
	
	public uint MinTime = 5;
	public uint MaxTime =  15;

	private GameObject curEnemy = null;
	private bool canSpawn = false;

	private void Start ()
	{
		curEnemy = GameObject.Instantiate(EnemyType[Random.Range(0, EnemyType.Length)], gameObject.transform.position, Quaternion.identity);
	}

	private void Update ()
	{
		if (curEnemy == null && !canSpawn)
		{
            StartCoroutine(nameof(ToSpawn));
        }
    }

	IEnumerator ToSpawn()
	{
		canSpawn = true;
	
		yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
		
		Debug.Log ("New enemy is spawing in position: " + transform.position);

		curEnemy = GameObject.Instantiate(EnemyType[Random.Range(0, EnemyType.Length)], gameObject.transform.position, Quaternion.identity);
		canSpawn = false;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + transform.up * 2);
	}
}
