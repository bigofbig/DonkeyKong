using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver current;
    public Action OnGameOver;
    public bool gameIsOver = false;
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
    }
}
