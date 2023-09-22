using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("ZombiePhysicsElement")]
    [SerializeField] private Rigidbody2D rb;

    [Header("ZombieAttributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform targetPoint;
    private int movePointCount= 0;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = EnemyManager.main.point[movePointCount];

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(targetPoint.position,transform.position) <= 0.1f)
        {
            movePointCount++;

            if (movePointCount == EnemyManager.main.point.Length)
            {
                ZombieSpawner.onEnemyKilledOrDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                targetPoint = EnemyManager.main.point[movePointCount];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 enemyDirection = (targetPoint.position - transform.position).normalized;
        rb.velocity = enemyDirection * moveSpeed;
    }
}