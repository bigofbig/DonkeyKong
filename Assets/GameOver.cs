using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver current;
    public Action OnGameOver;
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
        OnGameOver?.Invoke();
        TimeController.current.FreezeTheTimePermanetly();
        PlayerLives.current.remainLives -= 1;
        StartCoroutine(nameof(StartNewGameSession), 1);
        //call for next raound if live remain , keep up else bring restart menu
        //score 
    }

    IEnumerator StartNewGameSession()
    {
        yield return new WaitForSecondsRealtime(gameOverDuration);

        if(PlayerLives.current.remainLives>0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Called");
            //start new round with keeping scores
            //how to save score between two sessions?
            //Setup Ui
        }
        else
        {
            //ResetSocre
            //go to gameOver Screen
        }
    }
}
