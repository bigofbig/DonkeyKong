using System.Collections;
using UnityEngine;

public class JumpWithAnimEducation : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    void Start()
    {
        StartCoroutine(nameof(TweenToTarget), 2);
    }
    IEnumerator TweenToTarget(float duration)
    {
        float timePassed = 0;

        Vector3 start = transform.position;
        Vector3 end = transform.position + new Vector3(-2, 0);

        while (timePassed <= duration)
        {
            timePassed += Time.deltaTime;
            float time = timePassed / duration;
            float value = curve.Evaluate(time);
            //how can keep the postion value same and not cuaseing it just increasing --> by ading value to the lerp not the positoion

            transform.position = Vector3.Lerp(start, end, time) + new Vector3(0, value);
            yield return null;
        }
    }
}
