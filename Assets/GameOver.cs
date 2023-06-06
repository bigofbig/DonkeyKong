using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void OnGameOver()
    {
        Time.timeScale = 0;
    }
}
