using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TowerData))]
public class TowerDataEditor : Editor
{

    public override void OnInspectorGUI()
    {
        TowerData towerData = (TowerData)target;

        // Display a preview of the tower prefab if one is assigned
        if (towerData.towerPrefab != null)
        {
            GUIContent previewContent = new GUIContent(AssetPreview.GetAssetPreview(towerData.towerPrefab));

            // Center the preview image by placing it in a horizontal layout group
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Draw the preview image with the border style
            GUILayout.Label(previewContent, GUILayout.MaxHeight(100f), GUILayout.MaxWidth(100f));

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // Display the tower prefab field without a label
        EditorGUI.BeginChangeCheck();
        GameObject newTowerPrefab = (GameObject)EditorGUILayout.ObjectField(GUIContent.none, towerData.towerPrefab, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            // Update the tower prefab and force a repaint to display changes
            towerData.towerPrefab = newTowerPrefab;
            Repaint();
        }

        // Display other properties
        towerData.cost = EditorGUILayout.IntField("Cost", towerData.cost);
        towerData.damage = EditorGUILayout.IntField("Damage", towerData.damage);
        towerData.range = EditorGUILayout.FloatField("Range", towerData.range);
        towerData.attackSpeed = EditorGUILayout.FloatField("Attack Speed", towerData.attackSpeed);
        towerData.numberOfProjectiles = EditorGUILayout.IntField("Number of Projectiles", towerData.numberOfProjectiles);
        towerData.splashRadius = EditorGUILayout.FloatField("Splash Radius", towerData.splashRadius);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(towerData);
        }
    }
}
