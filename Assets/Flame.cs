using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 70;
    [SerializeField] float climbSpeed = 5;
    enum States { Wandering, Climb }
    States state = States.Wandering;

    float bound = 9;

    [Header("Climbing")]
    GameObject currentLadder;
    float ladderTop;
    float ladderDown;
    float speedOfMoveingToLadderXpos = 2;
    float minimumDistanceToclimb = .1f;
    float stopOffset = .7f;
    enum ClimbDirection { Up, Down }
    ClimbDirection climbDirection;

    [Header("LineCast")]
    [SerializeField] Vector3 linecastOffset;
    [SerializeField] Vector3 linecastEnd;
    LayerMask ladderMask;
    bool justFindALadderAbove = false;
    float belowRayCastCooldownDuration = 1;

    //going down or up move logic sepration
    //complete ladders invoke  climbing down too, when facing them
    //work on chance of using ladder , if needed seprate cjamce of climb down and up
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ladderMask = LayerMask.GetMask("Ladder");
        StartCoroutine(nameof(DirectionRandomizer));
    }
    void FixedUpdate()
    {
        //what about climbing down the ladders and also climbing down the broken ladders 
        //how to recogzine broken ladder, since their collider is shorter and there is no collider to trigger
        //-->we need a line cast with mask of ladder 
        //also at climbing end state we need to re decide if climb again or move along
        switch (state)
        {
            case States.Wandering:
                SetDirectionBasedOnBoundry();
                Move();
                break;
            case States.Climb:
                //get to ladder x pos
                bool isntCloseEnoughToLadder = Mathf.Abs(transform.position.x - currentLadder.transform.position.x) > minimumDistanceToclimb;
                if (isntCloseEnoughToLadder)
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(currentLadder.transform.position.x, transform.position.y), Time.fixedDeltaTime * speedOfMoveingToLadderXpos);
                    return;
                }
                MovingOnLadder();
                break;
        }
    }
    void Move()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y) * Time.deltaTime;
    }
    void MovingOnLadder()
    {
        switch (climbDirection)
        {
            case ClimbDirection.Up:
                if (transform.position.y < ladderTop + stopOffset)
                    transform.position += Vector3.up * climbSpeed * Time.fixedDeltaTime;
                else
                    ChangeState(States.Wandering);
                break;
            case ClimbDirection.Down:
                if (transform.position.y > ladderDown + stopOffset)
                    transform.position -= Vector3.up * climbSpeed * Time.fixedDeltaTime;
                else
                    ChangeState(States.Wandering);
                break;
        }
    }
    void Update()
    {
        if (state == States.Wandering)
            LadderBelowLineCast();
    }
    void LadderBelowLineCast()
    {
        if (justFindALadderAbove) return;
        RaycastHit2D hit = Physics2D.Linecast(transform.position + linecastOffset, transform.position + linecastEnd, ladderMask);
        Debug.DrawLine(transform.position + linecastOffset, transform.position + linecastEnd);
        if (hit)
        {
            justFindALadderAbove = true;
            Invoke(nameof(LadderBelowRayCastCooldown), belowRayCastCooldownDuration);
            //should i go down or not

            //this to  logic will be the same , should i  go down this ladder that i just climved && should i go down this ladder that i recently faced
            //if decided to go down
            if (true)
            {
                climbDirection = ClimbDirection.Down;
                SetLadderInfo(hit.collider.gameObject);
                ChangeState(States.Climb);
            }



        }
    }
    void SetDirectionBasedOnBoundry()
    {
        if (transform.position.x > bound)
        {
            moveSpeed = Mathf.Abs(moveSpeed * -1);
        }
        else if (transform.position.x < -bound)
        {
            //Debug.Log("BVoudn");
            moveSpeed = Mathf.Abs(moveSpeed);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //comn
        if (collision.CompareTag("Leader"))
        {
            if (collision.transform.position.y < transform.position.y) return;
            if (true)
            {
                climbDirection = ClimbDirection.Up;
                ChangeState(States.Climb);
                SetLadderInfo(collision.gameObject);
            }

        }
    }
    void SetLadderInfo(GameObject gameObject)
    {
        Ladder ladder = gameObject.GetComponent<Ladder>();
        currentLadder = gameObject;
        ladderTop = ladder.ladderEnd;
        ladderDown = ladder.ladderStart;
    }
    void ChangeState(States desiredState)
    {
        switch (desiredState)
        {

            case States.Wandering:
                rb.bodyType = RigidbodyType2D.Dynamic;
                ladderTop = 0;
                ladderDown = 0;
                currentLadder = null;
                break;

            case States.Climb:
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;

        }
        state = desiredState;
    }
    IEnumerator DirectionRandomizer()
    {
        yield return new WaitForSeconds(Random.Range(0, 15));
        moveSpeed *= -1;
        StartCoroutine(nameof(DirectionRandomizer));
    }

    void LadderBelowRayCastCooldown()
    {
        justFindALadderAbove = false;
    }

//    bool UsingLadderChance()
}

