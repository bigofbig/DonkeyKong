using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIndicator : MonoBehaviour
{
    List<GameObject> lifes = new List<GameObject>();
    void Start()
    {
        foreach (var item in lifes)
        {
            item.SetActive(false);
        }
        Debug.Log(PlayerLives.current.remainLives);
        for (int i = 0; i < PlayerLives.current.remainLives; i++)
        {
            lifes[i].SetActive(true);
        } 
    }
}
