using System.Collections.Generic;
using UnityEngine;
using System.IO;
using _levelProperties;

public static class GameInfos
{
    // Static fields to hold current game state
    private static int currentLevelIndex;
    private static int currentPlayerPoint;
    private static List<levelProperties> currentlevelItemsList = new List<levelProperties>();

    public static void setCurrentLevelIndex(int index)
    {
        currentLevelIndex = index;
    }
    public static int getCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
    public static void setCurrentPlayerPoint(int point)
    {
        currentPlayerPoint = point;
    }
    public static int getCurrentPlayerPoint()
    {
        return currentPlayerPoint;
    }
    public static void setcurrentlevelItemsList(List<levelProperties> levelItemList)
    {
        currentlevelItemsList = levelItemList;
    }
    public static List<levelProperties> getcurrentlevelItemsList()
    {
        return currentlevelItemsList;
    }
    // Method to save current game state
    public static void SaveGame(string filePath)
    {
        // Create a container for saving data
        GameSaveData saveData = new GameSaveData
        {
            currentLevelIndex = currentLevelIndex,
            currentPlayerPoint = currentPlayerPoint,
            levelItemsList = currentlevelItemsList
        };

        // Convert save data to JSON
        string json = JsonUtility.ToJson(saveData);

        // Write JSON to file
        File.WriteAllText(filePath, json);
        Debug.LogWarning("Save file saved: " + filePath);
    }

    // Method to load saved game state
    public static void LoadGame(string filePath)
    {
        if (File.Exists(filePath))
        {
            // Read JSON from file
            string json = File.ReadAllText(filePath);

            // Deserialize JSON into save data object
            GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(json);

            // Assign loaded data to current game state
            currentLevelIndex = saveData.currentLevelIndex;
            currentPlayerPoint = saveData.currentPlayerPoint;
            currentlevelItemsList = saveData.levelItemsList;
            Debug.LogWarning("Save file load: " + filePath);
        }
        else
        {
            Debug.LogWarning("Save file not found: " + filePath);
        }
    }

    // Nested class to hold save data structure
    [System.Serializable]
    private class GameSaveData
    {
        public int currentLevelIndex;
        public int currentPlayerPoint;
        public List<levelProperties> levelItemsList;
    }
}
