using System.Collections.Generic;
using UnityEngine;
using System.IO;
using _levelProperties;

public static class GameInfos
{
    // Static fields to hold current game state
    private static int currentLevelIndex=0;
    private static int currentPlayerPoint=0;
    private static List<levelProperties> currentlevelItemsList = new List<levelProperties>();

    public static void setCurrentLevelIndex(int index,bool save)
    {
        currentLevelIndex = index;
        if (save)
        {
            string filePath = Application.persistentDataPath + "/save.json";
            SaveGame(filePath);
        }
    }
    public static int getCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
    public static void setCurrentPlayerPoint(int point,bool save)
    {
        currentPlayerPoint = point;
        if (save)
        {
            string filePath = Application.persistentDataPath + "/save.json";
            SaveGame(filePath);
        }
    }
    public static int getCurrentPlayerPoint()
    {
        return currentPlayerPoint;
    }
    public static void setcurrentlevelItemsList(List<levelProperties> levelItemList,bool save)
    {
        currentlevelItemsList = levelItemList;
        if (save)
        {
            string filePath = Application.persistentDataPath + "/save.json";
            SaveGame(filePath);
        }

    }
    public static List<levelProperties> getcurrentlevelItemsList()
    {
        return currentlevelItemsList;
    }
    // Method to save current game state
    public static void SaveGame(string filePath)
    {
        // Create a container for saving data
        GameSaveData saveData = new GameSaveData();
        saveData.currentLevelIndex = currentLevelIndex;
        saveData.currentPlayerPoint = currentPlayerPoint;
        saveData.levelItemsList = getJsonLevelItms();

        // Convert save data to JSON
        string json = JsonUtility.ToJson(saveData);

        // Write JSON to file
        File.WriteAllText(filePath, json);
        //Debug.LogWarning("Save file saved: " + filePath);
    }

    static List<GameSaveDatalevelProperties> getJsonLevelItms()
    {
        List<GameSaveDatalevelProperties> levelsIDs = new List<GameSaveDatalevelProperties>();
        foreach (var item in currentlevelItemsList)
        {
            GameSaveDatalevelProperties jsonItem = new GameSaveDatalevelProperties
            {
                levelIndex = item.levelIndex,
                levelBGColor = item.levelBGColor,
                isUnlocked = item.isUnlocked,
                numberOfMovesLimit = item.numberOfMovesLimit,
                matrixID = getJsonLevelID(item.matrix)
            };
            levelsIDs.Add(jsonItem);
        }
        return levelsIDs;
    }

    static string getJsonLevelID(levelMatrix matrix)
    {
        if (matrix != null)
            return matrix.name;
        else
            return null;
    }

    public static void LoadGame(string filePath)
    {
        //Debug.Log("Try To Load");
        if (File.Exists(filePath))
        {
            // Read JSON from file
            string json = File.ReadAllText(filePath);

            // Deserialize JSON into save data object
            GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(json);

            // Check if deserialization worked and log data
            //Debug.Log("Loaded JSON: " + json);

            // Assign loaded data to current game state
            currentLevelIndex = saveData.currentLevelIndex;
            currentPlayerPoint = saveData.currentPlayerPoint;

            List<levelProperties> loadLevelFromJson = new List<levelProperties>();
            foreach (var item in saveData.levelItemsList)
            {
                levelProperties newItem = new levelProperties
                {
                    isUnlocked = item.isUnlocked,
                    levelBGColor = item.levelBGColor,
                    levelIndex = item.levelIndex,
                    numberOfMovesLimit = item.numberOfMovesLimit
                };

                //Debug.Log("Loading level: " + item.levelIndex + " with Matrix ID: " + item.matrixID);
                var matrixScr = Resources.Load<levelMatrix>("Level" + item.matrixID + "/" + item.matrixID);

                if (matrixScr != null)
                {
                    newItem.matrix = matrixScr;
                    //Debug.Log("Loaded Matrix with matrixID: " + item.matrixID);
                }
                else
                {
                    Debug.LogError("Matrix ScriptableObject not found for path: Level" + item.matrixID + "/" + item.matrixID);
                }

                loadLevelFromJson.Add(newItem);
            }

            currentlevelItemsList = loadLevelFromJson;
            //Debug.Log("Save file loaded: " + filePath);
        }
        else
        {
            Debug.Log("Save file not found: " + filePath);
        }
    }


    [System.Serializable]
    public class GameSaveDatalevelProperties
    {
        public int levelIndex;
        public int numberOfMovesLimit;
        public bool isUnlocked;
        public Color levelBGColor;
        public string matrixID;
    }

    [System.Serializable]
    public class GameSaveData
    {
        public int currentLevelIndex;
        public int currentPlayerPoint;
        public List<GameSaveDatalevelProperties> levelItemsList;
    }

}
