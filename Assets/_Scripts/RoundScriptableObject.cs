using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round", menuName = "Round Data")]
public class RoundScriptableObject : ScriptableObject
{
    public List<GameObject> enemies;
    [Range(0, 2)]
    public float spawnTime;

}
