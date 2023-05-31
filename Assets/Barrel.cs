using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
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
    enum State { Roll, Climb }

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
        //state transition configuration

        switch (desiredState)
        {
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

    void OnDrawGizmos()
    {
        if (currentState == State.Climb)
            Gizmos.DrawCube(transform.position + boxCastOffset, boxCastSize);
    }
}

