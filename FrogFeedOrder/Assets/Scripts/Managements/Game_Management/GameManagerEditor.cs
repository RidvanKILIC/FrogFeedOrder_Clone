using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private SerializedProperty positionObjectsProperty;
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = (GameManager)target;
        positionObjectsProperty = serializedObject.FindProperty("positionObjects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Display the grid of GameObjects
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("5x5 Matrix of GameObjects", EditorStyles.boldLabel);
        for (int row = 0; row < 5; row++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int col = 0; col < 5; col++)
            {
                int index = row * 5 + col;
                EditorGUI.BeginChangeCheck();
                GameObject obj = EditorGUILayout.ObjectField(gameManager.GetGameObject(row, col), typeof(GameObject), true) as GameObject;
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(gameManager, "Change GameObject");
                    gameManager.AddGameObject(obj, row, col);
                    EditorUtility.SetDirty(gameManager);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
