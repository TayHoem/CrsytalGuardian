using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{

    [Header("Enemy Number")]
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private int baseEnemies = 5;
    [SerializeField] private float enemiesPersecond = 1f;
    [SerializeField] private float timeBetweenWaves = 8f;
    [SerializeField] private float difficultyFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    public int enemiesAlive;
    public int enemyLeftSpawn;
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
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPersecond) && enemyLeftSpawn > 0)
        {
                SpawnEnemy();
                enemyLeftSpawn--;
                timeSinceLastSpawn = 0f;
        }
        
            if (enemiesAlive == 0 && enemyLeftSpawn == 0)
            {
                EndEnemyWave();
            }
        
    }

    public void enemyDestroyedOrKilled()
    {
        if (enemiesAlive != 0)
        {
            enemiesAlive--;
        }
    }

    private void SpawnEnemy()
    {
        if(TimerCounter.timeLimit > 180 && TimerCounter.timeLimit <= 300) {
            GameObject enemyToSpawn = enemys[0];
            GameObject enemyToSpawnRightUp = enemys[1];
            Instantiate(enemyToSpawn, EnemyManager.main.startingPoint[0].position, Quaternion.identity);
            Instantiate(enemyToSpawnRightUp, EnemyManager.main.startingPoint[1].position, Quaternion.identity);
        }else if(TimerCounter.timeLimit <= 180 && TimerCounter.timeLimit > 60)
        {
            GameObject enemyToSpawn = enemys[0];
            GameObject enemyToSpawnRightUp = enemys[1];
            GameObject enemyToSpawnRightDown = enemys[2];
            GameObject enemyToSpawnLeftUp = enemys[3];
            Instantiate(enemyToSpawn, EnemyManager.main.startingPoint[0].position, Quaternion.identity);
            Instantiate(enemyToSpawnRightUp, EnemyManager.main.startingPoint[1].position, Quaternion.identity);
            Instantiate(enemyToSpawnRightDown, EnemyManager.main.startingPoint[2].position, Quaternion.identity);
            Instantiate(enemyToSpawnLeftUp, EnemyManager.main.startingPoint[3].position, Quaternion.identity);
        }
        else if(TimerCounter.timeLimit >= 0 && TimerCounter.timeLimit <= 60)
        {
            GameObject enemyToSpawn = enemys[0];
            GameObject enemyToSpawnRightUp = enemys[1];
            GameObject enemyToSpawnRightDown = enemys[2];
            GameObject enemyToSpawnLeftUp = enemys[3];
            GameObject enemyToSpawnLeftDown = enemys[4];
            GameObject enemyToSpawnLeftStraight = enemys[5];
            Instantiate(enemyToSpawn, EnemyManager.main.startingPoint[0].position, Quaternion.identity);
            Instantiate(enemyToSpawnRightUp, EnemyManager.main.startingPoint[1].position, Quaternion.identity);
            Instantiate(enemyToSpawnRightDown, EnemyManager.main.startingPoint[2].position, Quaternion.identity);
            Instantiate(enemyToSpawnLeftUp, EnemyManager.main.startingPoint[3].position, Quaternion.identity);
            Instantiate(enemyToSpawnLeftDown, EnemyManager.main.startingPoint[4].position, Quaternion.identity);
            Instantiate(enemyToSpawnLeftStraight, EnemyManager.main.startingPoint[5].position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Game End");
        }
       
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemyLeftSpawn = EnemiesWave();
        if (TimerCounter.timeLimit > 180 && TimerCounter.timeLimit <= 300)
        {
            enemiesAlive = enemyLeftSpawn * 2;
        }
        else if (TimerCounter.timeLimit <= 180 && TimerCounter.timeLimit > 60)
        {
            enemiesAlive = enemyLeftSpawn * 4;
        }
        else
        {
            enemiesAlive = enemyLeftSpawn * 6;
        }
    }

    private int EnemiesWave()
    { 
            return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyFactor));

    }

    private void EndEnemyWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }
}
