using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveLoadManager : MonoBehaviour
{
    [SerializeField] levelManager _levelManager;
    private void OnEnable()
    {
        LoadGameState();
        if (GameInfos.getcurrentlevelItemsList()!= null && GameInfos.getcurrentlevelItemsList().Count > 0)
        {
            _levelManager.levelItemsList = GameInfos.getcurrentlevelItemsList();
            Debug.Log("_levelManager's Level List Updated");
        }
        else
        {
            GameInfos.setcurrentlevelItemsList(_levelManager.levelItemsList);
            Debug.Log("GameInfos' Level List Updated");
        }
        SaveGameState();
       
    }
    private void SaveGameState()
    {
        string filePath = Application.persistentDataPath + "/save.json";
        GameInfos.SaveGame(filePath);
    }

    private void LoadGameState()
    {
        string filePath = Application.persistentDataPath + "/save.json";
        if (!System.IO.File.Exists(filePath))
        {
            SaveGameState();
        }
        GameInfos.LoadGame(filePath);
    }
}
