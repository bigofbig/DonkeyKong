using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector3 wallCastlenght = new Vector3(1, 0);
    [SerializeField] Vector3 ladderCastlenght;
    [SerializeField] Vector3 ladderCastStartOffset;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask ladderMask;
    [SerializeField] LayerMask gridderMask;
    GameObject lastHitLadder;
    GameObject originGridder;
    float moveSeed = 4;
    enum State { Roll, Climb }
    State currentState = State.Roll;
    [SerializeField] Vector3 boxCastOffset;
    [SerializeField] Vector2 boxCastSize;
//barrelAnimation & random laddering
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
    void Roll()
    {
        rb.velocity = new Vector2(moveSeed, rb.velocity.y);
    }
    void Climb()
    {
        transform.position += Vector3.down * 2 * Time.fixedDeltaTime;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Roll:
                WallBoundry();
                LadderRaycast();
                break;
            case State.Climb:
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
                break;
        }
    }
    void OnDrawGizmos()
    {
        if (currentState == State.Climb)
            Gizmos.DrawCube(transform.position + boxCastOffset, boxCastSize);
    }
    void LadderRaycast()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position + ladderCastStartOffset, transform.position + ladderCastlenght, ladderMask);
        Debug.DrawLine(transform.position + ladderCastStartOffset, transform.position + ladderCastlenght);
        if (hit && lastHitLadder != hit.collider.gameObject)
        {
            lastHitLadder = hit.collider.gameObject;

            if (true)
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
                originGridder = null;
                RollOtherDirection();
                //rb static 
                rb.bodyType = RigidbodyType2D.Dynamic;
                //animation
                break;
            case State.Climb:
                //set transform
                //rb dynamic
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                //animation
                break;
        }
        currentState = desiredState;
    }
    void WallBoundry()
    {
        RaycastHit2D hit = Physics2D.Linecast(
            transform.position - wallCastlenght,
            transform.position + wallCastlenght, wallMask.value);
        if (hit.collider)
        {
            RollOtherDirection();
            //reverse move
        }
        Debug.DrawLine(transform.position + -wallCastlenght, transform.position + wallCastlenght, Color.cyan);
    }
    void RollOtherDirection()
    {
        moveSeed *= -1;
    }
}

