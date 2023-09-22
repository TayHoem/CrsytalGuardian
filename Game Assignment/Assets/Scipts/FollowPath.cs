using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoint;
    public Animator animator; //Enemy animator component
    public GameObject enemy;
    public float moveSpeed = 1f; //Enemy move speed 
    private int waypointIndex = 0; //Waypoint index
    Vector2 whereToSpawn;
    private float xWidth;
    private float yHeight;
    static int maxEnemy = 15; //maximum mo of enemy will spawn
    static float enemyCount = 0; //number of enemy on screen
    private int enemiesSpawned = 0; // Number of enemies spawned
    [SerializeField] private float interval = 2f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Assign the zombie position to the first waypoint position if 
        //it is not in the first waypoint at the beginning
        transform.position = waypoint[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (enemyCount < maxEnemy)
        {
            if (timer >= interval)
            {
                timer = 0f;
                Instantiate(enemy, transform.position, transform.rotation);
                enemyCount++;
            }
        }
    }

    private void followPath()
    {
        //if the waypoint index is less than the waypoint length
        if (waypointIndex < waypoint.Length)
        {
            //Move the zombie position to the waypoint position with the move speed
            transform.position = Vector3.MoveTowards(transform.position, waypoint[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == waypoint[1].transform.position)
            {
                // Randomly select a direction (1 for right, 2 for forward, 3 for left)
                int randomWaypoint = Random.Range(5, 8);
                followPath();
                checkAnimDirection(randomWaypoint);
            }

            //If the zombie position already at the waypoint position
            if (transform.position == waypoint[waypointIndex].transform.position)
            {
                //Move to the next waypoint position
                waypointIndex += 1;
            }
        }
    }

    //check the waypoint direction and set the animator "moveDir"
    //parameter values right = 2,left = 1,backward = 4,forward = 3
    private void checkAnimDirection(int waypoint)
    {
        if (waypoint == 5)
        {
            if (waypointIndex == 1)
                animator.SetInteger("moveDir", 1);
            else if (waypointIndex == 2)
                animator.SetInteger("moveDir", 4);
            else if (waypointIndex == waypoint)
                animator.SetInteger("moveDir", 1);
            else  //else move forward
                animator.SetInteger("moveDir", 3);
        }
        else if (waypoint == 6)
        {
            if (waypointIndex == waypoint)
                animator.SetInteger("moveDir", 1);
            else //else move left
                animator.SetInteger("moveDir", 1);
        }
        else
        {
            if (waypointIndex == 1)
                animator.SetInteger("moveDir", 1);
            else if (waypointIndex == 2)
                animator.SetInteger("moveDir", 3);
            else if (waypointIndex == waypoint)
                animator.SetInteger("moveDir", 1);
            else  //else move backward
                animator.SetInteger("moveDir", 4);
        }
    }
}
