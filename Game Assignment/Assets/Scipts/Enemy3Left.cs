using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Left : MonoBehaviour
{

    [Header("FishRobotPhysicsElement")]
    [SerializeField] private Rigidbody2D rb;

    [Header("FishRobotAttributes")]
    [SerializeField] private float moveSpeed = 3f;


    private Transform targetPoint;
    private int movePointCount = 0;
    void Start()
    {
        targetPoint = EnemyManager.main.pointUpLeft[movePointCount];

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(targetPoint.position, transform.position) <= 0.1f)
        {
            movePointCount++;

            if (movePointCount == EnemyManager.main.pointUpLeft.Length)
            {
                EnemySpawner.onEnemyKilledOrDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                targetPoint = EnemyManager.main.pointUpLeft[movePointCount];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 enemyDirection = (targetPoint.position - transform.position).normalized;
        rb.velocity = enemyDirection * moveSpeed;
    }
}
