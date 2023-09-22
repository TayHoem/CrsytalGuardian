using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;

    float expAmount = 250;
    LevelSystem levelSystem;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // 3-> 2 ->1 -> 0 = Enemy has died

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        ExperienceManager.Instance.AddExperince(expAmount);
        Destroy(this.gameObject);
    }


}
