using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPool : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] GameObject simpleBarrel;
    [SerializeField] GameObject blueBarrel;

    List<GameObject> barrelPool;
    List<GameObject> blueBarrelPool;

    public GameObject Barrel()
    {
        foreach (var barrel in barrelPool)
        {
            if (!barrel.activeInHierarchy)
            {
                barrel.SetActive(true);
                return barrel;
            }
        }
        GameObject instantiated = Instantiate(simpleBarrel);
        return instantiated;
    }
    public GameObject BlueBarrel()
    {
        foreach (var barrel in blueBarrelPool)
        {
            if (!barrel.activeInHierarchy)
            {
                barrel.SetActive(true);
                return barrel;
            }
        }
        GameObject instantiated = Instantiate(simpleBarrel);
        return instantiated;
    }
}
