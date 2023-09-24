using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBullet : MonoBehaviour
{
    [Header("Tower Bullet References")]
    [SerializeField] private Rigidbody2D rbBullet;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private AudioSource explosion;

    [Header("Tower Bullet Attributes")]
    [SerializeField] private float bulletSpeed = 10f;

    private Transform targetEnemy;
    // Start is called before the first frame update

    public void setTarget(Transform targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!targetEnemy) return;
        Vector2 direction = (targetEnemy.position - transform.position).normalized;

        rbBullet.velocity = direction * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(this.gameObject);
        explosion.Play();
        /// enemies to take damage
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyCompenent))
        {
            enemyCompenent.TakeDamage(1);
        }
    }
}
