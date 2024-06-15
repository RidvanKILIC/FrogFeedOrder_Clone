using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Matrix", menuName = "Level Matrices/New Level Matrix", order = 1)]
public class levelMatrix : ScriptableObject
{
    public List<levelMatrixCell> _levelMatrix = new List<levelMatrixCell>(25);
    // Method to get the index in the flat list from row and column
    private int GetIndex(int row, int col)
    {
        return row * 5 + col;
    }

    // Method to add a GameObject to the grid at specified row and column
    public void AddGameObject(levelMatrixCell obj, int row, int col)
    {
        int index = GetIndex(row, col);
        if (index >= 0 && index < 25) // Ensure index is within bounds
        {
            _levelMatrix[index] = obj;
        }
        else
        {
            Debug.LogError("Invalid index for adding GameObject to positionObjects list.");
        }
    }

    // Method to retrieve the GameObject at specified row and column
    public levelMatrixCell GetGameObject(int row, int col)
    {
        int index = GetIndex(row, col);
        if (index >= 0 && index < 25) // Ensure index is within bounds
        {
            return _levelMatrix[index];
        }
        else
        {
            Debug.LogError("Invalid index for retrieving GameObject from positionObjects list.");
            return null;
        }
    }
}
