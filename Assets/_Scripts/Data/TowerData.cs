using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


namespace TD {
    
    namespace Entity {

        [CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower Defense/Tower Data")]
        public class TowerData : ScriptableObject
        {
#region  Standard Values
            public int cost;
            public int damage;
            public float range;
            public float attackSpeed;
            public GameObject towerPrefab;
            public int numberOfProjectiles;
            public float splashRadius;
            public TowerUpgradeData towerUpgradeData;
            public Sprite upgradeSprite;
#endregion

#region Upgrade Values

            public int costToUpgrade;
            public int damageIncrease;
            public float rangeIncrease;
            public float attackSpeedIncrease;
            public int increaseOfProjectiles;
            public float splashRadiusIncrease;

#endregion

#region Public Functions

        public void Upgrade(TowerDataUpgrade _job)
            {
                switch (_job.type)
                {
                    case TowerUpgradeType.LEVEL1:
                    cost += costToUpgrade;
                    damage += damageIncrease;
                    range += rangeIncrease;
                    attackSpeed += attackSpeedIncrease;
                    numberOfProjectiles += increaseOfProjectiles;
                    splashRadius += splashRadiusIncrease;
                    break;

                    case TowerUpgradeType.LEVEL2:
                    cost += costToUpgrade;
                    damage += damageIncrease;
                    range += rangeIncrease;
                    attackSpeed += attackSpeedIncrease;
                    numberOfProjectiles += increaseOfProjectiles;
                    splashRadius += splashRadiusIncrease;
                    break;

                    case TowerUpgradeType.LEVEL3:
                    cost += costToUpgrade;
                    damage += damageIncrease;
                    range += rangeIncrease;
                    attackSpeed += attackSpeedIncrease;
                    numberOfProjectiles += increaseOfProjectiles;
                    splashRadius += splashRadiusIncrease;
                    break;
                }
            }       
       }

#endregion

        public class TowerDataUpgrade
        {
            public TowerUpgradeType type;

            public TowerDataUpgrade(TowerUpgradeType _type)
            {
                type = _type;
            }
        }
    }
}
