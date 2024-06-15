using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private levelManager _levelManager;

    // The list to hold GameObjects in a 5x5 grid
    [SerializeField] private List<GameObject> positionObjects = new List<GameObject>(25);
   
    // Method to get the index in the flat list from row and column
    private int GetIndex(int row, int col)
    {
        return row * 5 + col;
    }

    // Method to add a GameObject to the grid at specified row and column
    public void AddGameObject(GameObject obj, int row, int col)
    {
        int index = GetIndex(row, col);
        if (index >= 0 && index < 25) // Ensure index is within bounds
        {
            positionObjects[index] = obj;
        }
        else
        {
            Debug.LogError("Invalid index for adding GameObject to positionObjects list.");
        }
    }

    // Method to retrieve the GameObject at specified row and column
    public GameObject GetGameObject(int row, int col)
    {
        int index = GetIndex(row, col);
        if (index >= 0 && index < 25) // Ensure index is within bounds
        {
            return positionObjects[index];
        }
        else
        {
            Debug.LogError("Invalid index for retrieving GameObject from positionObjects list.");
            return null;
        }
    }

}
