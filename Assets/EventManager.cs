using UnityEngine;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public static UnityEvent<GameObject> OnGameobjectChange = new UnityEvent<GameObject>();
}
