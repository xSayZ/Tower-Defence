using UnityEngine;
using UnityEngine.Tilemaps;

namespace  TD {

    namespace Placement {

        public class PlacementController : MonoBehaviour {

            public static PlacementController instance;
            [SerializeField] private Camera sceneCamera;
            [SerializeField] private LayerMask placementLayermask;
            [SerializeField] private GameObject mouseIndicator;
            [SerializeField] private Grid grid;
            [SerializeField] private Tilemap tilemap;
            private Vector3 lastPos;

#region  Unity Functions
            private void Update() {
                Vector3 mousePosition = GetSelectedMapPosition();
                Debug.Log("Mouse Pos: "+mousePosition);
                mouseIndicator.transform.position = mousePosition;
            }

#endregion

#region Public Functions

#endregion

#region Private Functions
            private Vector3 GetSelectedMapPosition() {
                Vector3 mousePos = InputManager.GetInstance().GetMousePosition();

                // Convert mouse position to world space
                Vector3 worldMousePosition = ConvertScreenToIsometricWorld(mousePos);

                // Snap the world coordinates to the grid
                Vector3Int snappedMousePosition = grid.WorldToCell(worldMousePosition);

                // Check the tilemap or objects in the snapped grid cell
                // For example, if you're using a tilemap:
                TileBase tile = tilemap.GetTile(snappedMousePosition);

                // You can return information about the tile, or use it for interactions
                return snappedMousePosition;
            }

            private Vector2 ConvertScreenToIsometricWorld(Vector2 screenPos) {
                // Convert screen space to world space
                Vector3 worldPos = sceneCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, sceneCamera.nearClipPlane));

                // Adjust for isometric perspective
                float tileWidth = 11.5f; // Adjust this based on your tile size
                float tileHeight = 6.8f; // Adjust this based on your tile size

                // Isometric conversion
                float x = worldPos.x / tileWidth;
                float y = worldPos.y / tileHeight;

                float isoX = (x + y) / 2.0f;
                float isoY = (y - x) / 2.0f;

                return new Vector2(isoX, isoY);
            }           
#endregion
        }
    }
}
