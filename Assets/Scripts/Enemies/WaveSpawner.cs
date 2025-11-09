using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
[System.Serializable]
public class Wave
{
	public string waveName;
	public int numberOfEnemies;
	public GameObject[] typeOfEnemies;
	public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private Wave[] waves;
	[SerializeField] private Transform[] spawnPoints;

	private Wave currentWave;
	private int currentWaveNumber;
	private bool canSpawn = true;
	private float nextNextSpawnTime;

	private void Update()
	{
		currentWave = waves[currentWaveNumber];
		SpawnWave();
		GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
		{
			currentWaveNumber++;
			canSpawn = true;
		}
	}
	private void SpawnWave()
	{
		if (canSpawn && nextNextSpawnTime < Time.time)
		{
			GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
			Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
			Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
			currentWave.numberOfEnemies--;
			nextNextSpawnTime = Time.time + currentWave.spawnInterval;
			if (currentWave.numberOfEnemies == 0)
			{
				canSpawn = false;
			}
		}
	}
}