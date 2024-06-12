using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class PositionObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager manager = (GameManager)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Edit positionObjects Array");

        // Calculate the width of each cell to ensure consistent spacing
        float cellWidth = EditorGUIUtility.currentViewWidth / manager.positionObjects.GetLength(1);

        // Display each row of the array
        for (int i = 0; i < manager.positionObjects.GetLength(0); i++)
        {
            EditorGUILayout.BeginHorizontal();

            // Display each element in the row
            for (int j = 0; j < manager.positionObjects.GetLength(1); j++)
            {
                // Display the element with its position label
                string label = "[" + i + ", " + j + "]";
                EditorGUILayout.BeginVertical(GUILayout.Width(cellWidth));
                EditorGUILayout.LabelField(label, GUILayout.Width(cellWidth));
                manager.positionObjects[i, j] = (GameObject)EditorGUILayout.ObjectField(manager.positionObjects[i, j], typeof(GameObject), true, GUILayout.Width(cellWidth));
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
