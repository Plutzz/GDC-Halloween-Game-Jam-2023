using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    public int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    protected override void Awake()
    {
        base.Awake();
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
        Debug.Log(currentWave);
    }
    private void Update()
    {
        if (!isSpawning) { return; }

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0) 
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        GameObject _prefabToSpawn = enemyPrefabs[0];
        Instantiate(_prefabToSpawn, LevelManager.Instance.StartPoint.position, Quaternion.identity);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        Debug.Log(currentWave);
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        if(enemiesAlive < 0)
        {
            enemiesAlive = 0;
        }
    }

    private int EnemiesPerWave()
    {
        //Scale Difficulty
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    public int GetRounds()
    {
        return currentWave;
    }
}
