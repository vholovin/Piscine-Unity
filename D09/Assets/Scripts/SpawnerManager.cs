using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private GameObject[] spawner;
    private readonly string spawnerTag = "Spawner";

    public uint IncrementEnemyCount = 5;
    public uint IncrementEnemyHP = 10;
    public uint IncrementEnemyDamage = 5;

    public float[] TimeForWave = {30f, 40f, 50f, 60f, 70f};

    private float timer = 0f;
    private uint curCountEnemies = 0;
    private uint curWave = 0;
    private uint curWaveEnemies = 0;
    private uint curWaveMaxEnemies = 0;
    private bool isCanSpawn;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectsWithTag(spawnerTag);
    }

    private void Start()
    {
        ResetParameters();
    }

    private void ResetParameters()
    {
        timer = 0;
        curWaveEnemies = 0;
        curWaveMaxEnemies = (IncrementEnemyCount * (1 + curWave));
    }

    public void Update()
    {
        UpdateTimer();
        UpdateCurCountEnemies();
        UpdateCanSpawnEnemies();

        CheckWave();

        Debug.Log(curCountEnemies + " - " + curWaveEnemies + "/" + curWaveMaxEnemies);
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    private void UpdateCurCountEnemies()
    {
        curCountEnemies = 0;

        for (int i = 0; i < spawner.Length; i++)
        {
            curCountEnemies += (uint)((spawner[i].GetComponent<Spawner>().GetSpawnEnemy()) ? 1 : 0);
        }
    }

    private void UpdateCanSpawnEnemies()
    {
        isCanSpawn = (curWaveEnemies < curWaveMaxEnemies);
    }

    private void CheckWave()
    {
        if (timer < TimeForWave[curWave])
        {
            return;
        }

        if ((curWave + 1) < TimeForWave.Length)
        {
            curWave++;
            ResetParameters();
        }
    }

    public void AddEnemy()
    {
        curWaveEnemies++;
    }

    public bool IsCanSpawn()
    {
        return isCanSpawn;
    }

    public uint GetCurWave()
    {
        return (curWave + 1);
    }

    public uint GetMaxWave()
    {
        return (uint)TimeForWave.Length;
    }

    public uint GetIncrementEnemyHP()
    {
        return IncrementEnemyHP;
    }

    public uint GetIncrementEnemyDamage()
    {
        return IncrementEnemyDamage;
    }

    public float GetTimerWave()
    {
        return TimeForWave[curWave];
    }

    public float GetTimer()
    {
        return timer;
    }
}