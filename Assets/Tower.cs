using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private TowerData towerData;

    private int cost;
    private float damage;
    private float range;
    private int numberOfProjectiles;
    private float splashRadius;

    // Start is called before the first frame update
    private void Start()
    {
        if (towerData != null)
        {
            // Import tower data from the Scriptable Object
            cost = towerData.cost;
            damage = towerData.damage;
            range = towerData.range;
            numberOfProjectiles = towerData.numberOfProjectiles;
            splashRadius = towerData.splashRadius;

            // You can use this data to customize the tower's behavior or appearance
            Debug.Log("Cost: " + cost);
            Debug.Log("Damage: " + damage);
            Debug.Log("Range: " + range);
            Debug.Log("Number of Projectiles: " + numberOfProjectiles);
            Debug.Log("Splash Radius: " + splashRadius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
