using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower Defense/Tower Data")]
public class TowerData : ScriptableObject
{
    public int cost;
    public float damage;
    public float range;
    public GameObject towerPrefab;
    public int numberOfProjectiles;
    public float splashRadius;
}
