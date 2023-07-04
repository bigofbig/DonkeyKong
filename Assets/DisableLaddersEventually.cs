using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLaddersEventually : MonoBehaviour
{
    [SerializeField] List<GameObject> ladders;
    [SerializeField] float disableRate = 1;

    void Awake()
    {
        StartCoroutine(nameof(DisableLadders));
    }
    IEnumerator DisableLadders()
    {
        for (int i = 0; i < ladders.Count; i++)
        {
            if (i == 11) break;
            yield return new WaitForSeconds(disableRate);
            ladders[i].SetActive(false);
        }
        yield return new WaitForSeconds(.7f);
        foreach (var ladder in ladders)
        {
            if (ladder.activeInHierarchy)
                ladder.SetActive(false);
        }
    }
}
