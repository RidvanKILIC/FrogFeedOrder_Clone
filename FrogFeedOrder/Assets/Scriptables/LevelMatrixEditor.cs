using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(levelMatrix))]
public class LevelMatrixEditor : Editor
{
    private const int matrixSize = 5; // Size of the matrix (5x5 in this case)
    private const float cellSize = 80f; // Size of each cell in the grid
    private const float spacing = 10f; // Spacing between cells

    public override void OnInspectorGUI()
    {
        levelMatrix matrix = (levelMatrix)target;

        EditorGUILayout.LabelField("5x5 Level Matrix", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(GUI.skin.box);

        if (matrix._levelMatrix.Count != matrixSize * matrixSize)
        {
            Debug.LogWarning("Matrix size does not match expected 5x5 size. Initializing...");
            matrix._levelMatrix.Clear();

            // Initialize matrix with empty cells
            for (int i = 0; i < matrixSize * matrixSize; i++)
            {
                matrix._levelMatrix.Add(null); // Replace with your initialization logic if needed
            }
        }

        for (int i = 0; i < matrixSize; i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < matrixSize; j++)
            {
                int index = i * matrixSize + j;

                // Ensure index is valid
                if (index >= 0 && index < matrix._levelMatrix.Count)
                {
                    EditorGUILayout.BeginVertical(GUILayout.Width(cellSize), GUILayout.Height(cellSize));

                    // Draw cell content
                    EditorGUI.LabelField(new Rect(0, 0, cellSize, EditorGUIUtility.singleLineHeight), $"Cell ({i}, {j})", EditorStyles.centeredGreyMiniLabel);

                    // Assuming _levelMatrix is a List<levelMatrixCell>
                    matrix._levelMatrix[index] = EditorGUILayout.ObjectField(matrix._levelMatrix[index], typeof(levelMatrixCell), false) as levelMatrixCell;

                    EditorGUILayout.EndVertical();
                    GUILayout.Space(spacing); // Add spacing between cells
                }
                else
                {
                    Debug.LogError($"Index out of range: {index}. Ensure _levelMatrix is properly initialized and sized.");
                }
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(spacing); // Add spacing between rows
        }

        EditorGUILayout.EndVertical();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(matrix);
        }
    }
}
