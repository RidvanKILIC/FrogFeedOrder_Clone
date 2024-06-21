using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _levelProperties;
using _itemProperties;
public class levelSpawner : MonoBehaviour
{
    public float cellDistance;
    public float itmDistance;
    float currentDistance = 0;
    levelProperties currentLevel;
    List<levelMatrixCell> currentLevelMatrix;
    [SerializeField] itemList _itemList;
    [SerializeField] GameManager _gameManager;
    public List<levelGameObjectMatrix> currentLevelObjectMatrix = new List<levelGameObjectMatrix>(25);
    public List<levelGameObjects> nextLevelObjectsList = new List<levelGameObjects>();
    private void Awake()
    {
        //for (int i = 0; i < 25; i++)
        //{
        //    currentLevelObjectMatrix.Add(new levelGameObjectMatrix());
        //}
    }
    public void resetCurrentLevelObjectMatrix()
    {
        currentLevelObjectMatrix.Clear();
        for (int i = 0; i < 25; i++)
        {
            currentLevelObjectMatrix.Add(new levelGameObjectMatrix());
        }
    }
    public void DestroyCurrentLevelObjects()
    {
        for (int i = 0; i < 25; i++)
        {
            if (currentLevelObjectMatrix[i] != null)
            {
                for (int j = currentLevelObjectMatrix[i].levelObjs.Count-1; j >= 0; j--)
                {
                    if(currentLevelObjectMatrix[i].levelObjs[j] != null)
                    {
                        Destroy(currentLevelObjectMatrix[i].levelObjs[j].cellObj.gameObject);
                        Destroy(currentLevelObjectMatrix[i].levelObjs[j].itemObj.gameObject);
                    }
                }
            }
        }
        resetCurrentLevelObjectMatrix();
    }
    public void getCurrentLevel()
    {
        if (GameInfos.getcurrentlevelItemsList().ElementAt(GameInfos.getCurrentLevelIndex()) != null)
            currentLevel = GameInfos.getcurrentlevelItemsList().ElementAt(GameInfos.getCurrentLevelIndex());
        else
            Debug.Log("Current level is null");
    }
    public void spawnCurrentLevel()
    {
        _gameManager.isGameOver = false;
        nextLevelObjectsList.Clear();
        _gameManager.clearCurrentInteractedList();
        resetCurrentLevelObjectMatrix();
        getCurrentLevel();
        _gameManager.setnumberOfCount(currentLevel.numberOfMovesLimit);
        UIManager.UInstance.setCurrentLevelText(currentLevel.levelIndex);
        if (currentLevel.matrix._levelMatrix != null)
            currentLevelMatrix = currentLevel.matrix._levelMatrix;
        else
            Debug.Log("LevelMatrix is null");

        //Debug.Log("Current Level Matrix Count" + currentLevelMatrix.Count);
        for(int col = 0; col < 5; col++)
        {
            for(int row = 0; row < 5; row++)
            {
                currentDistance = 0;
                    if (currentLevelMatrix.ElementAt(row * 5 + col) != null)
                    {
                        List<_levelItem> currentCellItems = currentLevelMatrix.ElementAt(row * 5 + col).cellObjects;
                        for (int i=0;i<currentCellItems.Count; i++)
                        {
                            initAndSpawnItem(row, col,i,currentCellItems.ElementAt(i), i >= currentCellItems.Count-1 );
                        }
                    }
                    else
                    {
                        //Debug.Log(row + "," + col + " cell is null");
                    } 
              
            }
        }
    }
    private void initAndSpawnItem(int row , int col,int index,  _levelItem item,bool isLast)
    {
        //Debug.Log("Spawning");
        instantiateItem(_type.Cell,row, col, index, item,isLast);

        if (!item.itemObj.itemType.Equals(_type.Null))
        {
            instantiateItem(item.itemObj.itemType, row, col, index, item,isLast);
        }
        else
        {
            //Debug.Log("item obj is null");
        }
    }
    void instantiateItem(_type itemType,int row,int col, int index, _levelItem _item,bool isLast)
    {
        //Debug.Log(row+","+col+","+index + itemType);
        GameObject Obj = Instantiate(_itemList.getItemPrefab(itemType));
        Obj.GetComponent<Items>().setItemColor(itemType == _type.Cell ? _item.cellObj.cellColor : _item.itemObj.itemColor);
        Obj.transform.position = getPosition(itemType, row, col, isLast);
        Obj.GetComponent<Items>().setIndexes(row, col,index);
        Obj.GetComponent<Items>().setType(itemType);
        Obj.GetComponent<Items>().setSprite(_itemList.getItemTexture(itemType,itemType == _type.Cell ? _item.cellObj.cellColor : _item.itemObj.itemColor ));
        Obj.GetComponent<Items>().createAndSetMaterial();
        Obj.GetComponent<Items>().setMatarial();
        Obj.GetComponent<Items>().setRotation(itemType != _type.Cell ? _item.itemObj.itemRotation:_rotation.None);
        Obj.GetComponent<Items>().rotateObject(Obj);

        if (!itemType.Equals(_type.Cell) && !itemType.Equals(_type.Null))
        {
            if (itemType.Equals(_type.Fruit))
            {
                Obj.GetComponent<Items>().setPoint(1);
                _gameManager.increasePossiblePoint();
            }
            else if (itemType.Equals(_type.Frog))
            {
                Obj.name += _item.itemObj.itemColor;
            }    
            if (!isLast)
            {
                Obj.SetActive(false);
            }
                
        }
        addToObjectMatrix(row, col, Obj, itemType, index);
    }
    void addToObjectMatrix(int row, int col, GameObject obj, _type itemType, int index)
    {
        int matrixIndex = row * 5 + col;
        if (matrixIndex >= currentLevelObjectMatrix.Count)
        {
            Debug.LogError("Index out of bounds for currentLevelObjectMatrix");
            return;
        }

        var matrix = currentLevelObjectMatrix[matrixIndex];
        if (matrix == null)
        {
            matrix = new levelGameObjectMatrix();
            currentLevelObjectMatrix[matrixIndex] = matrix;
        }

        // Ensure the levelGameObjects instance exists for this cell
        while (matrix.levelObjs.Count <= index)
        {
            matrix.levelObjs.Add(new levelGameObjects());
        }

        var levelObj = matrix.levelObjs[index];

        if (itemType == _type.Cell)
        {
            levelObj.cellObj = obj;
        }
        else
        {
            levelObj.itemObj = obj;
        }
    }
    Vector3 getPosition(_type Type, int row, int col, bool isLast)
    {
        bool addDistance = false;//To add distance objects which are enabled on scene
        Vector3 basePos = _gameManager.getGameObject(row, col).transform.position;
        if (Type == _type.Cell)
            currentDistance += cellDistance;
        else if (Type != _type.Null)
        {
            if (isLast)
            {
                currentDistance += itmDistance;
            }
            else
            {
                addDistance = true;
                //Debug.Log("Add extra distance");
            }
                
        }
            
           

        basePos += addDistance ? new Vector3(0, currentDistance+itmDistance , 0): new Vector3(0, currentDistance, 0);
        return basePos;
    }
    levelGameObjects getCurrectLevelObjectAtPos(int row,int col,int index)
    {
        int matrixIndex = row * 5 + col;
        levelGameObjectMatrix matrixItm = currentLevelObjectMatrix.ElementAt(matrixIndex);
        //Debug.Log("Index: " + index + " List Count: " + matrixItm.levelObjs.Count);

            if (matrixItm != null && (index >= 0 && matrixItm.levelObjs.Count >= index))
            {
                return matrixItm.levelObjs.ElementAt(index);
            }
            else
                return null;
    }
    public void fillNextLevelObjects(List<GameObject> collecteds)
    {
        List<levelGameObjects> collectedLvObjs = new List<levelGameObjects>();
        for (int i = 0; i < collecteds.Count; i++)
        {
            int row = collecteds[i].GetComponent<Items>().rowIndex;
            int col = collecteds[i].GetComponent<Items>().columnIndex;
            int index = collecteds[i].GetComponent<Items>().listIndex-1;
            if (getCurrectLevelObjectAtPos(row, col, index) != null)
                collectedLvObjs.Add(getCurrectLevelObjectAtPos(row, col, index));
            //else
            //    Debug.Log("Item is null");
        }
        if (collectedLvObjs.Count > 0)
            nextLevelObjectsList = collectedLvObjs;
    }
    public void handleCollectedObjects(List<GameObject> collecteds)
    {
        fillNextLevelObjects(collecteds);
        List<levelGameObjects> collectedLvObjs = new List<levelGameObjects>();
        for (int i = 0; i < collecteds.Count; i++)
        {
            int row= collecteds[i].GetComponent<Items>().rowIndex;
            int col = collecteds[i].GetComponent<Items>().columnIndex;
            int index = collecteds[i].GetComponent<Items>().listIndex;
            if (getCurrectLevelObjectAtPos(row, col, index) != null)
            {
                getCurrectLevelObjectAtPos(row, col, index).collected = true;
                collectedLvObjs.Add(getCurrectLevelObjectAtPos(row, col, index));
            }  
            else
                Debug.Log("Item is null");
        }
        _gameManager.clearCurrentInteractedList();
        if (collectedLvObjs.Count > 0)
        {
            StartCoroutine(handleObjectAnims(collectedLvObjs));
            if (checkIsLevelFinished() && !_gameManager.isGameOver)
                Invoke("callGameOver", 6);
        }
            
        else
            Debug.Log("Collected object list is empty");
    }
    public bool checkIsLevelFinished()
    {
        if (_gameManager.getNumberOfCount() <= 0)
        {
            return true;
        }
        else
        {
            bool isFinished = true;
            for (int i = 0; i < currentLevelObjectMatrix.Count; i++)
            {
                if (currentLevelObjectMatrix[i].levelObjs != null)
                {
                    for (int j = 0; j < currentLevelObjectMatrix[i].levelObjs.Count; j++)
                    {
                        if (currentLevelObjectMatrix[i].levelObjs[j] != null)
                            if (!currentLevelObjectMatrix[i].levelObjs[j].collected)
                                isFinished = false;
                    }
                }
            }
            return isFinished;
        }
        
    }
    IEnumerator handleObjectAnims(List<levelGameObjects> items)
    {
        for(int i = items.Count-1; i >=0; i--)
        {
            if (items[i] != null)
            {
                if(items[i].itemObj!=null)
                    items[i].itemObj.SetActive(false);
                if (items[i].cellObj != null)
                    items[i].cellObj.SetActive(false);
            }

            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = nextLevelObjectsList.Count - 1; i >= 0; i--)
        {
            if (nextLevelObjectsList[i] != null)
            {
                if (nextLevelObjectsList[i].itemObj != null)
                    nextLevelObjectsList[i].itemObj.SetActive(true);
                if (nextLevelObjectsList[i].cellObj != null)
                    if (!nextLevelObjectsList[i].cellObj.activeInHierarchy)
                        nextLevelObjectsList[i].cellObj.SetActive(true);
            }
            yield return new WaitForSeconds(0.2f);
        }
        nextLevelObjectsList.Clear();
        _gameManager.setfrogsClicable(true);
    }
    public void callGameOver()
    {
            _gameManager.GameOver();
    }
    [System.Serializable]
    public class levelGameObjectMatrix
    {
        public List<levelGameObjects> levelObjs = new List<levelGameObjects>();
    }
    [System.Serializable]
    public class levelGameObjects
    {
        public bool collected = false;
        public GameObject cellObj;
        public GameObject itemObj;
    }

}
