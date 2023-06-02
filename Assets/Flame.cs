using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour
{
    //random going left and right 
    //ranadom decide to giong up nad down the ladders 

    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 70;
    [SerializeField] float climbSpeed = 5;
    enum States { Wandering, Climb }
    States state = States.Wandering;

    float bound = 9;

    [Header("Climbing")]
    GameObject currentLadder;
    float ladderEnd;
    float speedOfMoveingToLadderXpos = 2;
    float minimumDistanceToclimb = .1f;
    float stopOffset = .7f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(nameof(DirectionRandomizer));
    }
    void FixedUpdate()
    {
        //what about climbing down the ladders and also climbing down the broken ladders 
        //how to recogzine broken ladder, since their collider is shorter and there is no collider to trigger
        switch (state)
        {
            case States.Wandering:
                SetDirectionBasedOnBoundry();
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y) * Time.deltaTime;
                break;
            case States.Climb:
                //get to ladder x pos
                bool isntCloseEnoughToLadder = Mathf.Abs(transform.position.x - currentLadder.transform.position.x) > minimumDistanceToclimb;
                if (isntCloseEnoughToLadder)
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(currentLadder.transform.position.x, transform.position.y), Time.fixedDeltaTime * speedOfMoveingToLadderXpos);
                    return;
                }
                //go up
                if (transform.position.y < ladderEnd + stopOffset)
                    transform.position += Vector3.up * climbSpeed * Time.fixedDeltaTime;
                else
                    ChangeState(States.Wandering);
                break;
        }
    }
    void SetDirectionBasedOnBoundry()
    {
        if (transform.position.x > bound)
        {
            Debug.Log("BVoudn");
            moveSpeed = Mathf.Abs(moveSpeed * -1);
        }
        else if (transform.position.x < -bound)
        {
            Debug.Log("BVoudn");
            moveSpeed = Mathf.Abs(moveSpeed);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Leader"))
        {
            Debug.Log("Ladder for fire");
            if (true)
            {
                ChangeState(States.Climb);
                Ladder ladder = collision.GetComponent<Ladder>();
                Debug.Log(ladder);

                currentLadder = collision.gameObject;
                ladderEnd = ladder.leaderEnd;

                //start going up
                //rb to kinematic
                //get the end pos of ladder
                //it can immideitly goes down ladder randomly
                //will direction randomizer make conflict?
            }

        }
    }
    void ChangeState(States desiredState)
    {
        switch (desiredState)
        {

            case States.Wandering:
                rb.bodyType = RigidbodyType2D.Dynamic;
                ladderEnd = 0;
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
}
