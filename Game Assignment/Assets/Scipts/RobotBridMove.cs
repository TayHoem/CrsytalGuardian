using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBridMove : MonoBehaviour
{
    [Header("ZombiePhysicsElement")]
    [SerializeField] private Rigidbody2D rb;

    [Header("ZombieAttributes")]
    [SerializeField] private float moveSpeed = 5f;

    private Transform targetPoint;
    private int movePointCount = 0;
    void Start()
    {
        targetPoint = EnemyManager.main.pointUpRight[movePointCount];

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(targetPoint.position, transform.position) <= 0.1f)
        {
            movePointCount++;

            if (movePointCount == EnemyManager.main.pointUpRight.Length)
            {
                EnemySpawner.onEnemyKilledOrDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                targetPoint = EnemyManager.main.pointUpRight[movePointCount];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 enemyDirection = (targetPoint.position - transform.position).normalized;
        rb.velocity = enemyDirection * moveSpeed;
    }
}
