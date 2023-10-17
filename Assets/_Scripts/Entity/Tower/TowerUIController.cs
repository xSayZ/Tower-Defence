using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace TD {
    
    namespace Entity {

        public class TowerUIController : MonoBehaviour {
            // Members
            public bool debug;
            [Space]
            [SerializeField] private GameObject canvasParent;
            [SerializeField] private GameObject[] upgrades;
            //TowerDataUpgrade upgrade = new TowerDataUpgrade(TowerUpgradeType.NONE);


#region Unity Functions

            private void OnMouseOver() {
                if(MousePressed()){
                    canvasParent.SetActive(true);
                }
            }

            private void Update() {
                if(MousePressed() && !EventSystem.current.IsPointerOverGameObject()) {
                    canvasParent.SetActive(false);
                }   
            }


#endregion

#region Private Functions

            private bool MousePressed(){
                return InputManager.GetInstance().GetMousePressed();
            }

#endregion

#region  Public Functions

            public void HandleUpgradeClicked(GameObject _object){
                Log(""+_object);
            }

#endregion

            private void Log(string _msg) {
                if(!debug) return;
                Debug.Log("[Tower UI Controller]: "+_msg);
            }

            private void LogWarning(string _msg) {
                if(!debug) return;
                Debug.LogWarning("[Tower UI Controller]: "+_msg);
            }

        }
    }
}
