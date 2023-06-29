using System.Collections;
using UnityEngine;

public class Pauline : MonoBehaviour
{
    [SerializeField] Animator animator;
    int idleAnim = Animator.StringToHash("Idle");
    int screamAnim = Animator.StringToHash("Scream");
    [SerializeField] GameObject help;
    [SerializeField] GameObject heart;
    //pauline Behaviour
    //Mario Win (game finish by mario reaching the end of ladder)
    void Start()
    {
        StartCoroutine(nameof(CallForHelp));
        GameEvents.current.onWin += OnWin;
    }
    void OnWin()
    {
        StopCoroutine(nameof(CallForHelp));
        animator.Play(idleAnim);
        help.SetActive(false);
        heart.SetActive(true);
        AudioManager.current.Play("Win");
        AudioManager.current.Stop("Music");
    }
    IEnumerator CallForHelp()
    {
        help.SetActive(false);
        animator.Play(idleAnim);
        float idleDuration = Random.Range(3, 5f);
        yield return new WaitForSeconds(idleDuration);
        animator.Play(screamAnim);
        help.SetActive(true);
        float screamAnimDuration = 2;
        yield return new WaitForSeconds(screamAnimDuration);
        StartCoroutine(nameof(CallForHelp));
    }

}
