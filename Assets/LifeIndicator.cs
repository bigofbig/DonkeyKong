using System.Collections.Generic;
using UnityEngine;

public class LifeIndicator : MonoBehaviour
{
    [SerializeField] List<GameObject> lifes = new List<GameObject>();
    void Start()
    {
        foreach (var item in lifes)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < PlayerLives.current.remainLives; i++)
        {
            lifes[i].SetActive(true);
        }
    }
}
