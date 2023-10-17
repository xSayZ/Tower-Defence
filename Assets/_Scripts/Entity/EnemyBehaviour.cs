using System;
using System.Collections;
using System.Collections.Generic;
using TD.Core;
using UnityEngine;

namespace TD {

    namespace Entity { 
        
        public class EnemyBehaviour : MonoBehaviour {
            [SerializeField] private Sprite[] nextTier;
            [SerializeField] private float moveSpeed = 5f;
            [SerializeField] public int health = 1;
            private Transform[] waypoints;
            private int currentWaypointIndex;
            private SpriteRenderer spriteRenderer;

#region Unity Functions
            private void Awake() {
                Configure();
            }
            private void FixedUpdate() {
                HandleMovement();
            }
#endregion

#region Public Functions

            public void TakeDamage(int damage) {
                health -= damage;
                if (health <= 0) {
                    Destroy();
                } else {
                    spriteRenderer.sprite = nextTier[health - 1];
                }
            }

#endregion

#region Private Functions
            private void Configure(){
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                GameManager.GetInstance().RegisterEnemy(this);
                waypoints = PathInstance.Instance.waypoints;
            }

            private void HandleMovement() {
                if (currentWaypointIndex >= waypoints.Length) {
                    return;
                }

                Vector3 targetPosition = waypoints[currentWaypointIndex].position;
                float distanceToWaypoint = Vector3.Distance(transform.position, targetPosition);
                float stoppingDistance = 0.1f;

                if (distanceToWaypoint > stoppingDistance) {
                    MoveTowardsWaypoint(targetPosition, transform.position);
                } else {
                    HandleWaypointReached();
                }
            }

            private void MoveTowardsWaypoint(Vector3 targetPosition, Vector3 currentPosition) { 
                        Vector3 moveDirection = (targetPosition - currentPosition).normalized;
                        transform.position += moveDirection * moveSpeed * Time.fixedDeltaTime;
            }

            private void HandleWaypointReached() {
                currentWaypointIndex++;
                if(currentWaypointIndex >= waypoints.Length) {
                    Destroy();
                }
            }

            private void Destroy() {
                EventManager.OnGameobjectChange.Invoke(gameObject);
                GameManager.GetInstance().UnregisterEnemy(this);
                Destroy(gameObject);
            }
#endregion
        }

    }
}
