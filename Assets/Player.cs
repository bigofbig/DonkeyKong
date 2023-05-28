using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public StateManager stateManager;
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Components")]
    public int runAnim = Animator.StringToHash("Run");
    public int idleAnim = Animator.StringToHash("Idle");
    public int jumpAnim = Animator.StringToHash("Jump");
    public int landToIdleAnim = Animator.StringToHash("LandIdle");
    public int climbAnim = Animator.StringToHash("Climb");
    public int climStandAnim = Animator.StringToHash("ClimbStand");
    public int deadStandAnim = Animator.StringToHash("Dead");

    [Header("Ground Cast")]
    public Vector2 boxCastSize;//y is height and x is widht 
    public Vector2 boxCastOffset;

    [Header("Climbing")]
    public bool thisLeaderIsbroken;
    public float leaderXPos;
    public float leaderClimbEndPoint;
    public float leaderClimbStartPoint;
    public float leaderEnd;
    public GameObject currentLeader;
    public ClimbStates climbState;
    public enum ClimbStates { CantClimb, CanClimbUp, CanClimbDown }

    [Header("Properties")]
    public float runSpeed = 4;

    [Header("====>Debug<====")]
    [SerializeField] bool doDeath = true;

    void Awake()
    {
        stateManager = new StateManager(this);
        stateManager.Initialize(stateManager.idle);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (doDeath)
        {
            if (collision.gameObject.layer == 7)
            {//layer 7 is barrel
                stateManager.Transition(stateManager.dead);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Leader"))
        {
            currentLeader = collision.gameObject;
            //first time player reaches a leader
            SetClimbState();
            //how we undrestand when can climb up or down
            //set this when player stops climbing
            //set this when player ontrigger enter with leader 
            //set this when player ontrigger exit with leader 
            //remove can climb variable and put it into inum as a state
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Leader"))
        {
            currentLeader = null;
            SetClimbState();
        }
    }
    void Update()
    {
        stateManager.Update();
    }
    void FixedUpdate()
    {
        stateManager.FixedUpdate();
    }

    public void SetFaceing(bool toRight)
    {
        if (toRight)
            transform.rotation = new Quaternion(0, 0, 0, 0);
        else
            transform.rotation = new Quaternion(0, 180, 0, 0);

    }
    public void SetClimbState()
    {
        if (currentLeader == null)
        {
            climbState = ClimbStates.CantClimb;
            return;
        }

        float determineHeight = -1;
        if (currentLeader.transform.position.y - transform.position.y > determineHeight)
            climbState = ClimbStates.CanClimbUp;
        else
            climbState = ClimbStates.CanClimbDown;
    }
    public void GetCurrentLeaderInfo()
    {
        Leader leader = currentLeader.GetComponent<Leader>();
        thisLeaderIsbroken = leader.isBroken;
        leaderClimbEndPoint = leader.endPos;
        leaderClimbStartPoint = leader.startPos;
        leaderXPos = currentLeader.transform.position.x;
        leaderEnd = leader.leaderEnd;
    }
}
