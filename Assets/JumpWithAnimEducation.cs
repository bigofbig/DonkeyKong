using UnityEngine;

public class JumpWithAnimEducation : MonoBehaviour
{
    Rigidbody2D rb;
    enum JumpState { Jump, MidAir, Fall, Landed }
    [SerializeField] JumpState jumpstate;
    bool startLandCast = false;
    float targetHeight = 0;
    float midAirTimer = 0;

    [SerializeField] float jumpSpeed = 2;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float midAirSpeed = 1;
    [SerializeField] float midAirDuration = 2;
    [SerializeField] Vector3 lineEnd;
    [SerializeField] Vector3 lineStart;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //onenter
        InvokeRepeating("Jump", 2, 4);
    }
    void Update()
    {
        if (jumpstate != JumpState.Landed)
            rb.velocity = new Vector3(rb.velocity.x, 0);
        switch (jumpstate)
        {
            case JumpState.Jump:
                transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
                if (transform.position.y >= targetHeight)
                    jumpstate = JumpState.MidAir;
                break;
            case JumpState.MidAir:
                //it should be halfed becus its half up half down
                //first half up
                //split midairDUration in half
                //first half up second half down
                midAirTimer += Time.deltaTime;
                if (midAirTimer < midAirDuration / 2)
                    transform.position += Vector3.up * midAirSpeed * Time.deltaTime;
                else
                    transform.position += Vector3.down * midAirSpeed * Time.deltaTime;
                if (midAirTimer >= midAirDuration)
                {
                    midAirTimer = 0;
                    jumpstate = JumpState.Fall;
                }
                break;

            case JumpState.Fall:
                transform.position += Vector3.down * jumpSpeed * Time.deltaTime;

                RaycastHit2D hit = Physics2D.Linecast(transform.position + lineStart, transform.position + lineEnd);
                Debug.DrawLine(transform.position + lineStart, transform.position + lineEnd, Color.red);
                if (hit)
                    jumpstate = JumpState.Landed;
                break;
            case JumpState.Landed:

                break;
        }
    }

    void Jump()
    {
        if (jumpstate != JumpState.Landed) return;
        jumpstate = JumpState.Jump;
        targetHeight = transform.position.y + jumpHeight;
        midAirTimer = 0;
    }
}
