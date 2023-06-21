using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController current;
    void Awake()
    {
        current = this;
    }

    internal void FreezeTheTimePermanetly()
    {
        Time.timeScale = 0;
    }

    public void FreezeTheTimeTemperory(float duration)
    {
        StartCoroutine(nameof(TemperoryTimeFreezeProcces), duration);
    }
    IEnumerator TemperoryTimeFreezeProcces(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
}
