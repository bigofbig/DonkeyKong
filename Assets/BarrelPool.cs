using System.Collections.Generic;
using UnityEngine;

public class BarrelPool : MonoBehaviour
{
    public static BarrelPool current;
    [Header("Refrences")]
    [SerializeField] GameObject simpleBarrel;
    [SerializeField] GameObject blueBarrel;
    [Header("Pool")]
    List<GameObject> simpleBarrelPool = new List<GameObject>();
    List<GameObject> blueBarrelPool = new List<GameObject>();

    void Awake()
    {
        current = this;
    }
    public GameObject SimpleBarrel()
    {
        foreach (var barrel in simpleBarrelPool)
        {
            if (!barrel.activeInHierarchy)
            {
                barrel.SetActive(true);
                return barrel;
            }
        }
        GameObject instantiated = Instantiate(simpleBarrel);
        simpleBarrelPool.Add(instantiated);
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
        GameObject instantiated = Instantiate(blueBarrel);
        blueBarrelPool.Add(instantiated);
        return instantiated;
    }

}
