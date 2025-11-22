using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public GameObject[] heals;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Final Event Settings")]
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private GameObject[] objectsToEnable;

    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime;
    private void Start()
    {
        nextSpawnTime = Time.time;
    }
    private void Update()
    {
        if (currentWaveNumber >= waves.Length)
            return; 

        currentWave = waves[currentWaveNumber];

        if (canSpawn)
            SpawnWave();

        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (totalEnemies.Length == 0 && !canSpawn)
        {
            currentWaveNumber++;

            if (currentWaveNumber < waves.Length)
            {
                canSpawn = true;
            }
            else
            {
                TriggerFinalEvent();
            }
        }
    }


    private void SpawnWave()
    {
        if (currentWave.typeOfEnemies.Length == 0) return; 

        if (canSpawn && nextSpawnTime <= Time.time)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);

            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (currentWave.numberOfEnemies <= 0)
            {
                canSpawn = false;

                if (currentWave.heals.Length > 0)
                {
                    GameObject randomHeal = currentWave.heals[Random.Range(0, currentWave.heals.Length)];
                    if (randomHeal != null)
                        Instantiate(randomHeal, randomPoint.position, Quaternion.identity);
                }
            }
        }
    }



    private void TriggerFinalEvent()
    {
        foreach (var obj in objectsToEnable)
            if (obj != null) obj.SetActive(true);

        foreach (var obj in objectsToDisable)
            if (obj != null) obj.SetActive(false);

        enabled = false;
    }
}
