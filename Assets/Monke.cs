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

    [Header("Barrel Throw Logic")]
    bool barrelInstantiateTime = false;
    [SerializeField] Vector2 simpleBarrelSpawnPos;
    [SerializeField] Vector2 blueBarrelSpawnPos;
    void Start()
    {
        StartCoroutine(nameof(RollBlueBarrel));
    }
    IEnumerator RollSimpleBarrel()
    {
        animator.Play(rollBarrel);
        while (!barrelInstantiateTime)
        {
            yield return null;
        }
        GameObject barrel = BarrelPool.current.SimpleBarrel();
        barrel.transform.position = simpleBarrelSpawnPos;
        barrelInstantiateTime = false;
        float animChangeDelay = .2f;
        yield return new WaitForSeconds(animChangeDelay);
        animator.Play(idleStand);
        StartCoroutine(nameof(Standby));
    }
    IEnumerator RollBlueBarrel()
    {
        animator.Play(rollBlueBarrel);
        while (!barrelInstantiateTime)
        {
            yield return null;
        }
        GameObject barrel = BarrelPool.current.BlueBarrel();
        barrel.transform.position = blueBarrelSpawnPos;
        barrelInstantiateTime = false;
        float animChangeDelay = .2f;
        yield return new WaitForSeconds(animChangeDelay);
        StartCoroutine(nameof(Standby));
    }
    IEnumerator Standby()
    {
        animator.Play(idleStand);
        float idleDuration = Random.Range(0, 2f);
        yield return new WaitForSeconds(idleDuration);
        animator.Play(chestHit);
        float chestHitDuration = Random.Range(0, 2f);
        yield return new WaitForSeconds(chestHitDuration);
        StartCoroutine(nameof(Standby));
    }
    public void ItsTimeToThrowBarrel()
    {
        barrelInstantiateTime = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(simpleBarrelSpawnPos, .2f);
        Gizmos.DrawSphere(blueBarrelSpawnPos, .2f);
    }
}
