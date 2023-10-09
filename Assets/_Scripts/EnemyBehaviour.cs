using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stoppingDistance = 0.1f;

    private Transform[] waypoints;
    private int currentWaypointIndex;

    private void Start()
    {
        GameManager.GetInstance().RegisterEnemy(this);
        waypoints = PathInstance.Instance.waypoints;
    }

    private void FixedUpdate()
    {
        if(currentWaypointIndex < waypoints.Length)
        {
            Vector2 targetPosition = waypoints[currentWaypointIndex].transform.position;

            float distanceToWaypoint = Vector3.Distance(transform.position, new Vector3(waypoints[currentWaypointIndex].transform.position.x, waypoints[currentWaypointIndex].transform.position.y, 0));
            if(distanceToWaypoint > stoppingDistance)
            {
                Vector3 moveDirection = (new Vector3(targetPosition.x, targetPosition.y, 0) - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, 0), moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                currentWaypointIndex++;

                if(currentWaypointIndex >= waypoints.Length)
                {
                    GameManager.GetInstance().UnregisterEnemy(this);
                    Destroy(gameObject);
                }
            }
        }
    }
}
