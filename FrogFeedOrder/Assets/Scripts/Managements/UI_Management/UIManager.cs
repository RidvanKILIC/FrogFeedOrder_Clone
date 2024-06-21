using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text totalScoreTxt;
    [SerializeField] TMPro.TMP_Text currentScoreTxt;
    [SerializeField] TMPro.TMP_Text currentLevelTxt;
    [SerializeField] TMPro.TMP_Text numberOfMovesTxt;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject restartBtn;
    [SerializeField] GameObject nextLevelBtn;
    [SerializeField] GameObject quitBtn;
    [SerializeField] TMPro.TMP_Text gameOverText;
    [SerializeField] GameObject startPanel;
    private static UIManager instance;
    public static UIManager UInstance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UI instance is null");
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }
    public void setCurrentLevelText(int level)
    {
        currentLevelTxt.text = "Level: " + level.ToString();
    }
    public void setTotalScoreText(int score)
    {
        totalScoreTxt.text = score.ToString();
    }
    public void setCurrentScoreText(int score)
    {
        currentScoreTxt.text = score.ToString();
    }
    public void setNýmberOfMovesTxt(int moves)
    {
        numberOfMovesTxt.text = moves.ToString();
    }
    public void setGameOverText(string text)
    {
        gameOverText.text = text;
    }
    public void pauseGame()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().isGamePaused = true;
        PausePanel.SetActive(true);
    }
    public void resumeGame()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().isGamePaused = false;
        PausePanel.SetActive(false);
    }
    public void closeGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    public void gameOver(bool isSuccess,bool isLastLevel)
    {
        if (!isLastLevel)
        {
            if (isSuccess)
            {
                setGameOverText("Welldone!");
                restartBtn.SetActive(false);
                quitBtn.SetActive(false);
                nextLevelBtn.SetActive(true);

            }
            else
            {
                setGameOverText("Try Again");
                nextLevelBtn.SetActive(false);
                quitBtn.SetActive(false);
                restartBtn.SetActive(true);
            }
        }
        else
        {
            setGameOverText("Levels Completed!");
            nextLevelBtn.SetActive(false);
            restartBtn.SetActive(false);
            quitBtn.SetActive(true);
        }
       
        gameOverPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateStartPanel()
    {
        startPanel.SetActive(false);
    }
}
