using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public Action onGameOver;
    public Action onWin;
    public bool gameIsOver = false;
    float gameOverDuration = 4;
    void Awake()
    {
        current = this;
    }
    public void CallGameOver()
    {
        if (gameIsOver) return;
        gameIsOver = true;
        onGameOver?.Invoke();
        TimeController.current.FreezeTheTimePermanetly();
        PlayerLives.current.remainLives -= 1;
        StartCoroutine(nameof(StartNewGameSession), 1);
        //call for next raound if live remain , keep up else bring restart menu
        //score 
    }
    public void CallOnWin()
    {
        onWin?.Invoke();
    }

    IEnumerator StartNewGameSession()
    {
        yield return new WaitForSecondsRealtime(gameOverDuration);

        if (PlayerLives.current.remainLives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //start new round with keeping scores
            //how to save score between two sessions?
            //Setup Ui
        }
        else
        {
            ScoreCounter.score = 0;
            SceneManager.LoadScene("Menu");
        }
    }
}
