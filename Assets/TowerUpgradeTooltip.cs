using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace TD {

    namespace Entity {

        public class TowerUpgradeTooltip : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
            // Members
            [SerializeField] private GameObject tooltip;
            TowerUIController parent;

#region Unity Functions
            public void OnPointerEnter(PointerEventData eventData) {
               tooltip.SetActive(true);
            }
            
            public void OnPointerExit(PointerEventData eventData) {
                tooltip.SetActive(false);
            }

            public void OnPointerDown(PointerEventData eventData) {
                parent = GetComponentInParent<TowerUIController>();
                parent.HandleUpgradeClicked(gameObject);
            }
#endregion
        }
    }
}
