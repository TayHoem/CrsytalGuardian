using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ZombieSpawner : MonoBehaviour
{

    [Header("Enemy Number")]
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPersecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemyLeftSpawn;
    private bool isSpawning = false;

    [Header("Enemy Event")]
    public static UnityEvent onEnemyKilledOrDestroy = new UnityEvent(); 

    private void Awake()
    {
        onEnemyKilledOrDestroy.AddListener(enemyDestroyedOrKilled);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        //if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPersecond) && enemyLeftSpawn > 0)
        {
            SpawnEnemy();
            enemyLeftSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemyLeftSpawn == 0)
        {
            EndEnemyWave();
        }
    }

    private void enemyDestroyedOrKilled()
    {
        enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemys[0];
        Instantiate(enemyToSpawn, EnemyManager.main.startingPoint.position, Quaternion.identity);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemyLeftSpawn = EnemiesWave();
    }

    private void EndEnemyWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private int EnemiesWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyFactor));
    }
}
