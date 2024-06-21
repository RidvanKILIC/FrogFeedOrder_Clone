using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    public void pauseGame()
    {
        UIManager.UInstance.pauseGame();
    }
    public void resumeGame()
    {
        UIManager.UInstance.resumeGame();
    }
    public void restartLevel()
    {
        UIManager.UInstance.closeGameOverPanel();
        GameObject.Find("GameManager").gameObject.GetComponent<GameManager>().restartLevel();
    }
    public void nextLevel()
    {
        UIManager.UInstance.closeGameOverPanel();
        GameObject.Find("GameManager").gameObject.GetComponent<GameManager>().loadNextLevel();
    }
    public void startGame()
    {
        UIManager.UInstance.DeactivateStartPanel();
        GameObject.Find("GameManager").gameObject.GetComponent<GameManager>().startGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
