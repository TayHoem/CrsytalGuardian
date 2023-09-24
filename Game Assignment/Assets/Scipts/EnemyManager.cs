using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
     
    [Header("WayPoint Right")]
    public static EnemyManager main;

    public Transform startingPoint;
    public Transform[] point;
    public Transform[] pointUpRight;
    public Transform[] pointDownRight;

    [Header("WayPoint Left")]
    public Transform leftStartingPoint;
    public Transform[] pointStraightLeft;
    public Transform[] pointUpLeft;
    public Transform[] pointDownLeft;

    private void Awake()
    {
        main = this;

    }
}
