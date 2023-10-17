using System.Collections.Generic;
using UnityEngine;

namespace TD {
    
    namespace Entity {
        
        public class TowerController : MonoBehaviour {

            // Members
            [SerializeField] private TowerData towerData;
            private List<GameObject> detectedTargets = new List<GameObject>();
            [SerializeField] public TowerTargeting towerTargeting = TowerTargeting.First;
            public bool debug;
            private GameObject target = null;
            

#region Internal Variables

            private int cost;
            private int damage;
            private float range;
            private float attackSpeed;
            private int numberOfProjectiles;
            private float splashRadius;
            float attackCooldown;

#endregion

            public enum TowerTargeting {
                First,
                Last,
                Close,
                Strong
            }

            private class AttackJob {
                public TowerTargeting targeting;
                public AttackJob(TowerTargeting _targeting){
                    targeting = _targeting;
                }
            }

#region Unity Function

            private void Awake() {
                if (towerData == null) return;

                Configure();
            }

            private void Update() {
                DetectTargets();
                AttackTarget();
            }

            private void OnDrawGizmos() {
                if (!debug) return;
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, range);
            }

#endregion

#region Public Functions

            public void AttackTarget() {
                AddAttackJob(new AttackJob(towerTargeting));
            }

            public void OnUpgrade(TowerDataUpgrade _type){
                AddUpgradeJob(_type);
            }


#endregion

#region Private Functions

            private void Configure() {
                cost = towerData.cost;
                damage = towerData.damage;
                range = towerData.range;
                attackSpeed = towerData.attackSpeed;
                numberOfProjectiles = towerData.numberOfProjectiles;
                splashRadius = towerData.splashRadius;
                attackCooldown = 0f;
            }

            private void DetectTargets() {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);

                foreach (Collider2D collider in colliders)
                {
                    if (!collider.gameObject.CompareTag("Target")) return;

                    GameObject _target = collider.gameObject;

                    if (detectedTargets.Contains(_target)) return;
                    
                    detectedTargets.Add(_target);

                    if (Vector3.Distance(_target.transform.position, transform.position) > range) {
                        detectedTargets.Remove(_target);
                    }
                }

            }

            private void AddAttackJob(AttackJob _job) {
                RunAttackJob(_job);
                Log("Starting job on ["+_job+"] with operation "+_job.targeting);
            }

            private void RunAttackJob(AttackJob _job){

                switch (_job.targeting)
                {
                    case TowerTargeting.First:
                        target = GetFirstTargetGameObject();
                    break;

                    case TowerTargeting.Last:
                        // Implement Last Target Function
                    break;

                    case TowerTargeting.Close:
                        // Implement Close Target Function
                    break;

                    case TowerTargeting.Strong:
                        // Implement Strong Target Function
                    break;
                }

                if (Time.time >= attackCooldown) {
                    if (target == null) return;
                    Vector3 dir = new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y);
                    EventManager.OnGameobjectChange.AddListener(RemoveTargetFromList);
                    target.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                    attackCooldown = Time.time + 1f / attackSpeed;
                }
            }

            private void AddUpgradeJob(TowerDataUpgrade _job){
                RunUpgradeJob(_job);
            }

            private void RunUpgradeJob(TowerDataUpgrade _job){
                towerData.Upgrade(_job);
                Configure();
            }

            private void RemoveTargetFromList(GameObject _obj) {
                detectedTargets.Remove(_obj);
                Log("Removing ["+_obj+"] from list");
            }  

            private GameObject GetFirstTargetGameObject(){
                if (detectedTargets.Count <= 0) return null;

                return detectedTargets[0].gameObject;
            }

            private void Log(string _msg) {
                if(!debug) return;
                Debug.Log("[Tower Controller]: "+_msg);
            }

            private void LogWarning(string _msg) {
                if(!debug) return;
                Debug.LogWarning("[Tower Controller]: "+_msg);
            }

#endregion

        }
    }
}
