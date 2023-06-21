using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public StateManager stateManager;
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Animations")]
    [HideInInspector] public int runAnim = Animator.StringToHash("Run");
    [HideInInspector] public int idleAnim = Animator.StringToHash("Idle");
    [HideInInspector] public int jumpAnim = Animator.StringToHash("Jump");
    [HideInInspector] public int landToIdleAnim = Animator.StringToHash("LandIdle");
    [HideInInspector] public int climbAnim = Animator.StringToHash("Climb");
    [HideInInspector] public int climStandAnim = Animator.StringToHash("ClimbStand");
    [HideInInspector] public int deadStandAnim = Animator.StringToHash("Dead");
    [HideInInspector] public int hammerIdleAnim = Animator.StringToHash("HammerIdle");
    [HideInInspector] public int hammerRunAnim = Animator.StringToHash("HammerRun");

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
    [Header("Climbing Deathcast")]//becuase while climbing rb goes kinematic
    public float deathSphereRadius;
    [SerializeField] bool visualizeDeathSphereCast = false;

    [Header("Jumping")]
    public float jumpSpeed = 2;
    public float jumpHeight = 2;
    public float midAirSpeed = 1;
    public float midAirDuration = 2;

    [Header("properties")]
    public float runSpeed = 4;
    float bound = 9.5f;

    [Header("HammerTime")]
    public bool isHammerTime = false;
    float hammerTimeDuration = 15;
    [Header("HammerSphereCast")]
    public bool visualizeHammerSphereCast = false;
    [SerializeField] float hammerSphereCastRadius = 1.5f;
    [SerializeField] float hammerCastXOffset = .5f;
    public LayerMask layersOfEnemies;

    [Header("====>Debug<====")]
    [SerializeField] bool doDeath = true;

    void Awake()
    {
        stateManager = new StateManager(this);
        stateManager.Initialize(stateManager.idle);
    }
    void OnPlayerDeath()
    {
        stateManager.Transition(stateManager.dead);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (doDeath)
        {
            if (collision.gameObject.layer == 10 || collision.gameObject.layer == 7 || collision.gameObject.layer == 9)
            {
                //layer 7 is barrel 9 is flame 10 is fallingBarrel
                OnPlayerDeath();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            Destroy(collision.gameObject);
            Invoke(nameof(HammerTimeIsOver), hammerTimeDuration);
            isHammerTime = true;
        }
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
        KeepPlayerInBoundry();
    }
    void KeepPlayerInBoundry()
    {
        Vector2 marioPos = transform.position;
        marioPos.x = Mathf.Clamp(marioPos.x, -bound, bound);
        transform.position = marioPos;
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
        Ladder leader = currentLeader.GetComponent<Ladder>();
        thisLeaderIsbroken = leader.isBroken;
        leaderClimbEndPoint = leader.endPos;
        leaderClimbStartPoint = leader.startPos;
        leaderXPos = currentLeader.transform.position.x;
        leaderEnd = leader.ladderEnd;
    }
    void HammerTimeIsOver()
    {
        isHammerTime = false;
    }
    public void HammerDestoryingCast()
    {
        bool facingRight = transform.rotation.y == 0;
        if (facingRight)
            hammerCastXOffset = Mathf.Abs(hammerCastXOffset);
        else
            hammerCastXOffset = Mathf.Abs(hammerCastXOffset) * -1;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position + new Vector3(hammerCastXOffset, 0), hammerSphereCastRadius, Vector2.zero, 0, layersOfEnemies);
        if (hit)
        {
            TimeController.current.FreezeTheTimeTemperory(1);
            GameObject deathVFX = BarrelPool.current.EnemyDeathVFX();
            deathVFX.transform.position = hit.collider.gameObject.transform.position;
            //if barrel disable
            //if flame destory
            Flame isFlame = hit.collider.gameObject.GetComponent<Flame>();
            if (isFlame)
            {
                Destroy(isFlame.gameObject);
                ScoreCounter.current.AddScore(800);
            }
            else
            {
                hit.collider.gameObject.SetActive(false);
                ScoreCounter.current.AddScore(500);
            }
        }
    }
    void OnDrawGizmos()
    {
        if (visualizeDeathSphereCast)
        {
            if (stateManager.currentState == stateManager.climb)
                Gizmos.DrawSphere(transform.position, deathSphereRadius);
        }
        Gizmos.color = new Color(1, 1, 1, .2f);
        if (visualizeHammerSphereCast)
        {
            if (stateManager.currentState == stateManager.hammerIdle || stateManager.currentState == stateManager.hammerRun)
                Gizmos.DrawSphere(transform.position + new Vector3(hammerCastXOffset, 0), hammerSphereCastRadius);
        }
    }
}
