using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private levelManager _levelManager;
    // The list to hold GameObjects in a 5x5 grid
    [SerializeField] private List<GameObject> positionObjects = new List<GameObject>(25);
    [SerializeField]  levelSpawner _spawner;
    bool frogsClicable = true;
    public bool isGameOver = false;
    public bool isGamePaused = false;
    int numberOfCount = 0;
    int possiblePoint = 0;
    int pointEarned = 0;
    List<GameObject> currentInteractedObjs = new List<GameObject>();
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        frogsClicable = true;
        isGameOver = false;
        numberOfCount = 0;
        possiblePoint = 0;
        pointEarned = 0;
        _spawner = GetComponent<levelSpawner>();
    }
    private void Start()
    {
       
    }
    public void startGame()
    {
        _spawner.spawnCurrentLevel();
    }
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
            UnityEngine.Debug.LogError("Invalid index for adding GameObject to positionObjects list.");
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
            UnityEngine.Debug.LogError("Invalid index for retrieving GameObject from positionObjects list.");
            return null;
        }
    }

    public GameObject getGameObject(int row,int col)
    {
        return positionObjects.ElementAt(GetIndex(row, col));
    }
    public void setfrogsClicable(bool state)
    {
        frogsClicable = state;
    }
    public bool getfrogsClicable()
    {
        return frogsClicable;
    }
    public void setnumberOfCount(int number)
    {
        UIManager.UInstance.setNýmberOfMovesTxt(number);
        numberOfCount = number;
    }
    public int getNumberOfCount()
    {
        return numberOfCount;
    }
    public void decreaseNumberOfCount()
    {
        numberOfCount--;
        UIManager.UInstance.setNýmberOfMovesTxt(numberOfCount);
        if (numberOfCount <= 0)
            if (!_spawner.checkIsLevelFinished())
                GameOver();
    }
    public void setPossiblePoint(int number)
    {
        possiblePoint = number;
    }
    public void increasePossiblePoint()
    {
        possiblePoint++;
    }
    public void addItemToCurrentInteractedList(GameObject obj)
    {
        currentInteractedObjs.Add(obj);
        //Debug.Log("Items Added" + obj.name);
    }
    public void clearCurrentInteractedList()
    {
        currentInteractedObjs.Clear();
        currentInteractedObjs = new List<GameObject>();
    }
    public void setPointEarned(int point)
    {
        pointEarned = point;
    }
    public int getPointEarned()
    {
        return pointEarned;
    }
    public void increasePointEarned()
    {
        pointEarned++;
    }
    public void collectInteractables()
    {
        //Debug.Log("Collecting Stuff");
        _spawner.handleCollectedObjects(currentInteractedObjs);
        //for(int i = 0; i < currentInteractedObjs.Count; i++)
        //{
        //    currentInteractedObjs[i].gameObject.SetActive(false);
        //}
    }
    public void loadNextLevel()
    {
        _spawner.spawnCurrentLevel();
    }
    public void restartLevel()
    {
        _spawner.spawnCurrentLevel();
    }
    public void GameOver()
    {
        if (!isGameOver)
        {
            clearCurrentInteractedList();
            UnityEngine.Debug.Log(new StackFrame(1, true).GetMethod().Name);
            isGameOver = true;
            _spawner.DestroyCurrentLevelObjects();
            //Debug.Log("Estimated Point:"+pointEarned+" "+"PossiblePoint: "+possiblePoint);
            if (pointEarned >= (possiblePoint / 2))
            {
                //Debug.Log("Current Index:  "+ GameInfos.getCurrentLevelIndex() +" Next Index: " + (GameInfos.getCurrentLevelIndex() + 1) + "size: " + GameInfos.getcurrentlevelItemsList().Count);
                int nextLEvel = GameInfos.getCurrentLevelIndex() + 1;
                //Debug.Log(nextLEvel);
                if (nextLEvel < GameInfos.getcurrentlevelItemsList().Count)
                {
                    GameInfos.setCurrentLevelIndex(nextLEvel, true);
                    GameInfos.getcurrentlevelItemsList().ElementAt(GameInfos.getCurrentLevelIndex()).isUnlocked = true;
                    UIManager.UInstance.gameOver(true, false);
                }
                else
                {
                    UIManager.UInstance.gameOver(true, true);
                }
            }
            else
            {
                UIManager.UInstance.gameOver(false, false);
            }
        }
       
    }

}
