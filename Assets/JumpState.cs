using UnityEngine;

public class JumpState : Istate
{
    Player player;
    float jumpY = 5;
    bool startLandCasting = false;
    LayerMask mask = LayerMask.GetMask("Ground");
    public JumpState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.animator.Play(player.jumpAnim);
        player.rb.AddForce(Vector2.up * jumpY, ForceMode2D.Impulse);

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
        player.animator.Play(player.landToIdleAnim);
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        Land();
    }

    void Land()
    {
        if (player.rb.velocity.y < 0 && !startLandCasting)
            startLandCasting = true;
        if (!startLandCasting) return;
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)player.transform.position + player.boxCastOffset, player.boxCastSize, 0, Vector2.zero, 0, mask);
        Color c = new Color();
        if (hit.collider != null)
        {
            c = Color.green;
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
