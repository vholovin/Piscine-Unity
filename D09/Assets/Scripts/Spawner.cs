using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Spawner : MonoBehaviour
{
    private SpawnerManager manager;
    private readonly string managerName = "SpawnerManager";

    public GameObject[] EnemyType;

    private GameObject enemy = null;
    private bool isSpawn = false;

    public uint MinTimeDelay = 1;
    public uint MaxTimeDelay = 10;

    private void Awake()
    {
        GameObject managerFind;
        managerFind = GameObject.Find(managerName);

        manager = managerFind.GetComponent<SpawnerManager>();
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (enemy == null && !isSpawn)
        {  
            StartCoroutine(nameof(ToSpawn));
        }
    }

    IEnumerator ToSpawn()
    {
        isSpawn = true;

        yield return new WaitForSeconds(Random.Range(MinTimeDelay, MaxTimeDelay));


        if (manager.IsCanSpawn())
        {
            manager.AddEnemy();

            enemy = Instantiate(EnemyType[Random.Range(0, EnemyType.Length)], gameObject.transform.position, Quaternion.identity);

            enemy.GetComponent<Enemy>().addHP(manager.GetIncrementEnemyHP() * (manager.GetCurWave() - 1));
            enemy.GetComponent<Enemy>().addDamage(manager.GetIncrementEnemyDamage() * (manager.GetCurWave() - 1));

            Debug.Log("New enemy is spawing in position: " + enemy.transform.position);
        }

        isSpawn = false;
    }

    public bool GetSpawnEnemy()
    {
        return (enemy) ? true : false; 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2);
    }
}