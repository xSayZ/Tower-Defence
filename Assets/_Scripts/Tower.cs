using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private TowerData towerData;
    public List<GameObject> detectedTargets = new List<GameObject>();
    private GameObject target = null;

    private int cost;
    private int damage;
    private float range;
    private float attackSpeed;
    private int numberOfProjectiles;
    private float splashRadius;

    [SerializeField] private bool attackFirst, attackClose, attackFar, attackLast;
    [SerializeField] private float attackCooldown;

    // Start is called before the first frame update
    private void Start()
    {
        attackFirst = true;

        if (towerData != null)
        {
            // Import tower data from the Scriptable Object
            cost = towerData.cost;
            damage = towerData.damage;
            range = towerData.range;
            attackSpeed = towerData.attackSpeed;
            numberOfProjectiles = towerData.numberOfProjectiles;
            splashRadius = towerData.splashRadius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectTargets();
    }

    void DetectTargets()
    {
        target = null;

        // Get all GameObjects within the specified radius.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);

        // Iterate over the array of GameObjects and check if each one has an Enemy tag.
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Target")
            {
                GameObject targetTransform = collider.gameObject;

                // Check if this enemy is already in the list
                if (!detectedTargets.Contains(targetTransform))
                {
                    // Add the enemy to the list
                    detectedTargets.Add(targetTransform);
                }
            }

            // Remove any targets outside of the specified radius
            if (Vector3.Distance(collider.transform.position, transform.position) > range)
            {
                detectedTargets.Remove(collider.gameObject);
            }
        }

        if (detectedTargets.Count > 0 && detectedTargets[0] != null)
        {
            target = GetFirstTargetTransform();
            AttackTarget();
        }
    }

    public void AttackTarget()
    {
        if (attackFirst)
        {
            target = GetFirstTargetTransform();
        }

        if(Time.time >= attackCooldown)
        {
            Debug.Log("Attacking target");
            Vector3 dir = new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y);
            Debug.DrawLine(transform.position, dir, Color.red);

            EventManager.OnGameobjectChange.AddListener(RemoveTargetFromList);
            target.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);

            attackCooldown = Time.time + 1f / attackSpeed;
        }

    }

    void RemoveTargetFromList(GameObject targetObj)
    {
        Debug.Log("Removing " + targetObj);
        detectedTargets.Remove(targetObj);
    }

    // Function to get the transform of the first detected enemy
    public GameObject GetFirstTargetTransform()
    {
        if (detectedTargets.Count > 0)
        {
            return detectedTargets[0].gameObject;
        }
        else
        {
            return null; // Return null if no enemies are detected.
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wireframe circle in the Scene view to represent the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

