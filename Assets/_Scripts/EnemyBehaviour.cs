using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite[] nextTier;
    [SerializeField] private float moveSpeed = 5f;
    private float stoppingDistance = 0.1f;

    [SerializeField] public int health;

    private Transform[] waypoints;
    private int currentWaypointIndex;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        GameManager.GetInstance().RegisterEnemy(this);
        waypoints = PathInstance.Instance.waypoints;
    }

    private void FixedUpdate()
    {
        if(currentWaypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            Vector3 currentPosition = transform.position;

            float distanceToWaypoint = Vector3.Distance(currentPosition, targetPosition);

            if (distanceToWaypoint > stoppingDistance)
            {
                // Calculate the movement direction and normalize it
                Vector3 moveDirection = (targetPosition - currentPosition).normalized;
                transform.position += moveDirection * moveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                currentWaypointIndex++;

                if(currentWaypointIndex >= waypoints.Length)
                {
                    Destroy();
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy();
        }
        else
        {
            spriteRenderer.sprite = nextTier[health - 1];
        }
    }

    void Destroy()
    {
        EventManager.OnGameobjectChange.Invoke(gameObject);
        Debug.Log("Destroying " + gameObject);
        GameManager.GetInstance().UnregisterEnemy(this);
        Destroy(gameObject);
    }
}
