using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController current;
    void Awake()
    {
        current = this;
    }
    public void TimeFreezeRequest(float duration)
    {
        StartCoroutine(nameof(TemperoryTimeFreezeProcces), duration);
    }
    IEnumerator TemperoryTimeFreezeProcces(float duration)
    {
        Debug.Log("Time freeze");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Debug.Log("Time unfreeze");
        Time.timeScale = 1;
    }
}
