using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [Header("WayPoint")]
    public static EnemyManager main;

    public Transform startingPoint;
    public Transform[] point;
    public Transform[] pointUpRight;
    public Transform[] pointDownRight;

    private void Awake()
    {
        main = this;

    }
}
