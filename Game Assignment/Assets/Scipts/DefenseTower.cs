using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DefenseTower : MonoBehaviour
{
    [Header("Tower References")]
    [SerializeField] private Transform towerRotationPoint; //point to rotat the tower
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject towerBulletStore;
    [SerializeField] private Transform towerFirePoint;
    [SerializeField] private AudioSource towerShootSound;
    

    [Header("Tower Attribute")]
    [SerializeField] private float targetRange = 5f; //distance to target the enemy
    [SerializeField] private float rotatingSpeed = 5f;
    [SerializeField] private float bps = 1f; //bullets in a second

    private Transform targetE;
    private float timeFire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetE == null)
        {
            FindTargetEnemy();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetInRange())
        {
            targetE = null;
        }
        else
        {
            timeFire += Time.deltaTime;

            if(timeFire >= 1f/bps)
            {
                Shoot();
                timeFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(towerBulletStore, towerFirePoint.position, towerFirePoint.rotation);
        towerBullet bullet1 = bullet.GetComponent<towerBullet>();
        towerShootSound.Play();
        bullet1.setTarget(targetE);
    }


    private bool CheckTargetInRange()
    {
        return Vector2.Distance(targetE.position, transform.position) <= targetRange;
    }
    private void FindTargetEnemy()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange,
                              (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            targetE = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(targetE.position.y - transform.position.y, targetE.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        towerRotationPoint.rotation = Quaternion.RotateTowards(towerRotationPoint.rotation, targetRotation, rotatingSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        //draw a 2D circle
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
    }
}
