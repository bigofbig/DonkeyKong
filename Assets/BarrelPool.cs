using System.Collections.Generic;
using UnityEngine;

public class BarrelPool : MonoBehaviour
{
    public static BarrelPool current;
    [Header("Refrences")]
    [SerializeField] GameObject simpleBarrel;
    [SerializeField] GameObject blueBarrel;
    [SerializeField] GameObject enemyDeathVFX;
    [Header("Pool")]
    List<GameObject> simpleBarrelPool = new List<GameObject>();
    List<GameObject> blueBarrelPool = new List<GameObject>();
    List<GameObject> EnemyDeathVFXPool = new List<GameObject>();
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

    public GameObject EnemyDeathVFX()
    {
        foreach (var vfx in EnemyDeathVFXPool)
        {
            if (!vfx.activeInHierarchy)
            {
                vfx.SetActive(true);
                return vfx;
            }
        }
        GameObject instantiated = Instantiate(enemyDeathVFX);
        EnemyDeathVFXPool.Add(instantiated);
        return instantiated;
    }
}
