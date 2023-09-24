using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Left : MonoBehaviour
{
    [Header("BirdRobotPhysicsElement")]
    [SerializeField] private Rigidbody2D rb;

    [Header("BirdRobotAttributes")]
    [SerializeField] private float moveSpeed = 5f;

    private Transform targetPoint;
    private int movePointCount = 0;
    void Start()
    {
        targetPoint = EnemyManager.main.pointDownLeft[movePointCount];

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(targetPoint.position, transform.position) <= 0.1f)
        {
            movePointCount++;

            if (movePointCount == EnemyManager.main.pointDownLeft.Length)
            {
                EnemySpawner.onEnemyKilledOrDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                targetPoint = EnemyManager.main.pointDownLeft[movePointCount];
            }


        }
    }

    private void FixedUpdate()
    {
        Vector2 enemyDirection = (targetPoint.position - transform.position).normalized;
        rb.velocity = enemyDirection * moveSpeed;
    }
}
