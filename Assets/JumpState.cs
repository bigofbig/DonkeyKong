using UnityEngine;

public class JumpState : Istate
{
    Player player;
    float jumpY = 5;
    bool startLandCasting = false;
    LayerMask mask = LayerMask.GetMask("Ground");
    LayerMask barrelMask = LayerMask.GetMask("Barrel");
    bool jumpedABarrel = false;

    enum State { Jump, MidAir, Fall }
    State state;
    float targetHeight = 0;
    float midAirTimer = 0;
    public JumpState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {

        AudioManager.current.Play("Jump");
        //Reseting temp Variables
        state = State.Jump;
        targetHeight = player.transform.position.y + player.jumpHeight;
        midAirTimer = 0;
        player.animator.Play(player.jumpAnim);

        bool staticJump = player.rb.velocity.x < .1f;
        if (staticJump) return;
        bool jumpRight = (player.transform.rotation.y == 0);
        if (jumpRight)
            player.rb.velocity = new Vector2(player.runSpeed, player.rb.velocity.y);
        else
            player.rb.velocity = new Vector2(-player.runSpeed, player.rb.velocity.y);
    }
    public void OnExit()
    {
        startLandCasting = false;
        jumpedABarrel = false;
        player.animator.Play(player.landToIdleAnim);
    }
    public void OnFixedUpdate()
    {
    }
    public void OnUpdate()
    {
        player.rb.velocity = new Vector3(player.rb.velocity.x, 0);
        switch (state)
        {
            case State.Jump:
                player.transform.position += Vector3.up * player.jumpSpeed * Time.deltaTime;
                if (player.transform.position.y >= targetHeight)
                    state = State.MidAir;
                break;
            case State.MidAir:
                midAirTimer += Time.deltaTime;
                if (midAirTimer < player.midAirDuration / 2)
                    player.transform.position += Vector3.up * player.midAirSpeed * Time.deltaTime;
                else
                    player.transform.position += Vector3.down * player.midAirSpeed * Time.deltaTime;
                if (midAirTimer >= player.midAirDuration)
                {
                    midAirTimer = 0;
                    state = State.Fall;
                }
                break;
            case State.Fall:
                player.transform.position += Vector3.down * player.jumpSpeed * Time.deltaTime;
                break;
        }
        Land();
        BarrelJumpScore();
    }
    void BarrelJumpScore()
    {
        if (jumpedABarrel) return;
        Color color = new Color();
        RaycastHit2D hit = Physics2D.Linecast(player.transform.position, player.transform.position + Vector3.down * 2, barrelMask);
        if (hit.collider)
        {
            color = Color.green;
            jumpedABarrel = true;
            ScoreCounter.current.AddScore(100);
            AudioManager.current.Play("BarrelJump");
        }
        else
            color = Color.red;

        Debug.DrawLine(player.transform.position, player.transform.position + Vector3.down * 2, color);
    }
    void Land()
    {
        if (state != State.Jump && !startLandCasting)
            startLandCasting = true;
        if (!startLandCasting) return;
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)player.transform.position + player.boxCastOffset, player.boxCastSize, 0, Vector2.zero, 0, mask);
        Color c = new Color();
        if (hit.collider != null)
        {
            c = Color.green;
            if (player.isHammerTime)
            {
                player.stateManager.Transition(player.stateManager.hammerIdle);
                AudioManager.current.Play("HammerTime");
                return;
            }
            player.stateManager.Transition(player.stateManager.idle);
        }
        else
            c = Color.gray;

        float width = player.boxCastSize.x;
        float height = player.boxCastSize.y;
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(-width / 2, height / 2), new Vector2(width, 0), c);
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(-width / 2, -height / 2), new Vector2(width, 0), c);
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(-width / 2, -height / 2), new Vector2(0, height), c);
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(width / 2, -height / 2), new Vector2(0, height), c);
    }
}
