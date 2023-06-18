using UnityEngine;

public class HammerRunState : Istate
{
    Player player;
    bool playerShouldFaceRight;
    LayerMask mask = LayerMask.GetMask("Ground");

    public HammerRunState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        playerShouldFaceRight = Input.GetKey(KeyCode.D);
        player.SetFaceing(playerShouldFaceRight);
        player.animator.Play(player.hammerRunAnim);
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnFixedUpdate()
    {
        if (playerShouldFaceRight)
            player.rb.velocity = new Vector2(1 * player.runSpeed, player.rb.velocity.y);
        else
            player.rb.velocity = new Vector2(-1 * player.runSpeed, player.rb.velocity.y);
    }

    public void OnUpdate()
    {

        if (!player.isHammerTime)
        {
            player.stateManager.Transition(player.stateManager.idle);
            return;
        }

        RaycastHit2D hit = Physics2D.BoxCast((Vector2)player.transform.position + player.boxCastOffset, player.boxCastSize, 0, Vector2.zero, 0, mask);
        Color c = new Color();
        if (hit.collider != null)
        {
            c = Color.green;
        }
        else
            player.stateManager.Transition(player.stateManager.hammerIdle);

        float width = player.boxCastSize.x;
        float height = player.boxCastSize.y;
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(-width / 2, height / 2), new Vector2(width, 0), c);
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(-width / 2, -height / 2), new Vector2(width, 0), c);
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(-width / 2, -height / 2), new Vector2(0, height), c);
        Debug.DrawRay((Vector2)player.transform.position + player.boxCastOffset + new Vector2(width / 2, -height / 2), new Vector2(0, height), c);

        if (playerShouldFaceRight && !Input.GetKey(KeyCode.D))
        { player.stateManager.Transition(player.stateManager.hammerIdle); return; }
        else if (!playerShouldFaceRight && !Input.GetKey(KeyCode.A))
        { player.stateManager.Transition(player.stateManager.hammerIdle); return; }
    }
}
