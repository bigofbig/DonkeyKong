using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D _collider;
    float moveSeed = 4;

    [Header("Casting")]
    [SerializeField] Vector3 ladderCastlenght;
    [SerializeField] Vector3 ladderCastStartOffset;
    [SerializeField] LayerMask ladderMask;
    [SerializeField] LayerMask gridderMask;
    [SerializeField] Vector3 boxCastOffset;
    [SerializeField] Vector2 boxCastSize;
    GameObject lastHitLadder;
    GameObject originGridder;

    [Header("State")]
    State currentState = State.Roll;
    enum State { Roll, Climb, Ignite, Fall }

    [Header("Animation")]
    [SerializeField] Animator animator;
    int barrelRollAnim = Animator.StringToHash("Roll");
    int barrelClimbAnim = Animator.StringToHash("BarrelClimb");
    int chanseOfNotClimbingAladder = 4;

    [Header("Boundry")]
    [SerializeField] float boundry;
    float firstFloorHeight = -9;
    float barrelDiableHeight = -13;

    [Header("Redirection")]
    bool justRedirected = false;
    float redirectCoolDown = 1;

    [Header("BarrelIgnite")]
    [SerializeField] bool IgnitableBarrel = false;
    [SerializeField] AnimationCurve curve;

    [Header("BarrelFall")]
    [SerializeField] bool isFallingVectically = false;
    float fallSpeed = 2;
    [SerializeField] Vector3 fallingCastStartOffset;
    [SerializeField] Vector3 fallingCastEnd;

    //1.125
    //-0.875
    //-4.625
    //-6.75
    //final grider -10
    //falling ignitable barrel should be same logic with rolling one both of them should use one ending logic for converting to flame
    void Awake()
    {
        if (isFallingVectically)
            ChangeState(State.Fall);

    }
    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Roll:
                Roll();
                break;
            case State.Climb:
                Climb();
                break;
        }
    }
    void Update()
    {
        switch (currentState)
        {
            case State.Fall:
                RaycastHit2D hit = Physics2D.Linecast(transform.position + fallingCastStartOffset, transform.position + fallingCastEnd, gridderMask);
                Debug.DrawLine(transform.position + fallingCastStartOffset, transform.position + fallingCastEnd);
                if (hit)
                    transform.position += Vector3.down * fallSpeed/2 * Time.deltaTime;
                else
                    transform.position += Vector3.down * fallSpeed * Time.deltaTime;
                break;

            case State.Roll:
                Boundry();
                LadderLineCast();
                break;
            case State.Climb:
                ReachingLadderEndCast();
                break;

        }
    }
    void Boundry()
    {
        if (gameObject.transform.position.y > firstFloorHeight)
        {
            if (gameObject.transform.position.x >= boundry || gameObject.transform.position.x <= -boundry)
                RollOtherDirection();
        }
        else if (gameObject.transform.position.y < barrelDiableHeight)
            gameObject.SetActive(false);

        //a contidion for ignitable barrels check for specific  if barrel was on specic y value check for x value
        if (!IgnitableBarrel) return;
        if (transform.position.y <= -9.8f && transform.position.x <= -5.87f)
        {
            if (currentState == State.Ignite) return;

            //call curve animation
            ChangeState(State.Ignite);
            StartCoroutine(nameof(Jump), 1);
        }

    }
    void Roll()
    {
        rb.velocity = new Vector2(moveSeed, rb.velocity.y);
    }
    void Climb()
    {
        transform.position += Vector3.down * 2 * Time.fixedDeltaTime;
    }
    void ReachingLadderEndCast()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + boxCastOffset, boxCastSize, 0, Vector2.zero, 0, gridderMask);
        if (hit)
        {
            if (originGridder == null)
                originGridder = hit.collider.gameObject;

            if (hit.collider.gameObject != originGridder)
            {
                originGridder = hit.collider.gameObject;
                ChangeState(State.Roll);
            }
        }
    }
    void LadderLineCast()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position + ladderCastStartOffset, transform.position + ladderCastlenght, ladderMask);
        Debug.DrawLine(transform.position + ladderCastStartOffset, transform.position + ladderCastlenght);
        if (hit && lastHitLadder != hit.collider.gameObject)
        {
            lastHitLadder = hit.collider.gameObject;

            if (Random.Range(0, chanseOfNotClimbingAladder) == 0)
            {
                ChangeState(State.Climb);
                //change the state
            }
        }
    }
    void ChangeState(State desiredState)
    {
        if (currentState == State.Fall)
            _collider.enabled = true;

        switch (desiredState)
        {
            case State.Fall:
                rb.bodyType = RigidbodyType2D.Kinematic;
                _collider.enabled = false;
                break;
            case State.Roll:
                animator.Play(barrelRollAnim);
                rb.bodyType = RigidbodyType2D.Dynamic;
                originGridder = null;
                RollOtherDirection();
                break;
            case State.Climb:
                animator.Play(barrelClimbAnim);
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            case State.Ignite:
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
        }
        currentState = desiredState;
    }
    void RollOtherDirection()
    {
        if (!justRedirected)
        {
            moveSeed *= -1;
            justRedirected = true;
            Invoke(nameof(RedirectCoolDown), redirectCoolDown);

        }
    }
    void RedirectCoolDown()
    {
        justRedirected = false;
    }

    IEnumerator Jump(float duration)
    {
        float timePassed = 0;

        Vector3 start = transform.position;
        float jumpWidth = 2.2f;
        Vector3 end = transform.position + new Vector3(-jumpWidth, 0);

        while (timePassed <= duration)
        {
            timePassed += Time.deltaTime;
            float time = timePassed / duration;
            float value = curve.Evaluate(time);
            //how can keep the postion value same and not cuaseing it just increasing --> by ading value to the lerp not the positoion

            transform.position = Vector3.Lerp(start, end, time) + new Vector3(0, value);
            yield return null;
        }
        // done , now instantiate a  
        GameObject flame = Instantiate(ObjectPool.current.flame, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    void OnDrawGizmos()
    {
        if (currentState == State.Climb)
            Gizmos.DrawCube(transform.position + boxCastOffset, boxCastSize);
    }
}

