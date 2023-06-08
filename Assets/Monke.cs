using System.Collections;
using UnityEngine;

public class Monke : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Animator animator;
    int idleStand = Animator.StringToHash("Idle");
    int chestHit = Animator.StringToHash("ChestHit");
    int rollBarrel = Animator.StringToHash("RollBarrel");
    int rollBlueBarrel = Animator.StringToHash("RollBlueBarrel");
    bool barrelInstantiateTime = false;
    private void Start()
    {
        StartCoroutine(nameof(RollBarrel));
    }
    IEnumerator RollBarrel()
    {
        animator.Play(rollBarrel);
        while (!barrelInstantiateTime)
        {
            yield return null;
        }
        //instantiate barrel in proprite pose
        
        Debug.Log("Barrel");
        barrelInstantiateTime = false;
    }
    public void ItsTimeToThrowBarrel()
    {
        barrelInstantiateTime = true;
    }
}
