using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInstance : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoint positions

    // Singleton pattern to easily access this script from other scripts
    public static PathInstance Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
