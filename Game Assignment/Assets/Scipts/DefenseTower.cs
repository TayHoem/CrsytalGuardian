using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DefenseTower : MonoBehaviour
{
    [Header("Tower References")]
    [SerializeField] private Transform towerRotationPoint; //point to rotat the tower
    [SerializeField] private LayerMask enemyMask;
    //[SerializeField] private GameObject towerBullet;
    //[SerializeField] private Transform towerFirePoint;
    

    [Header("Tower Attribute")]
    [SerializeField] private float targetRange = 5f; //distance to target the enemy
    [SerializeField] private float rotatingSpeed = 5f;

    private Transform targetE;

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
        //towerRotationPoint.rotation = targetRotation;
        towerRotationPoint.rotation = Quaternion.RotateTowards(towerRotationPoint.rotation, targetRotation, rotatingSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        //draw a 2D circle
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
    }
}
