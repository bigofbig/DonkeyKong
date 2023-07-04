using System.Collections;
using UnityEngine;

public class OnEnableDisableWithDelay : MonoBehaviour
{
    [SerializeField] float disableDelay;
    void OnEnable()
    {
        StartCoroutine(nameof(DisableWithDelay));
    }
    IEnumerator DisableWithDelay()
    {
        yield return new WaitForSecondsRealtime(disableDelay);
        gameObject.SetActive(false);
    }
}

