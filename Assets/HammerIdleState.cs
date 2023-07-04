using UnityEngine;

public class HammerIdleState : Istate
{
    Player player;
    bool isGounded = false;
    LayerMask mask = LayerMask.GetMask("Ground");
    public HammerIdleState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {
        player.rb.velocity = Vector2.zero;
        player.animator.Play(player.hammerIdleAnim);
    }
    public void OnExit()
    {
    }
    public void OnFixedUpdate()
    {
    }
    public void OnUpdate()
    {

        if (!player.isHammerTime)
        {
            player.stateManager.Transition(player.stateManager.idle);
            return;
        }

        IsGrounded();

        if (!isGounded) return;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            player.stateManager.Transition(player.stateManager.hammerRun);

        //sphere cast 

        player.HammerDestoryingCast();
        //dont forget to puase time
        //call enemy destroy and add Score using score indicator


    }


    void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)player.transform.position + player.boxCastOffset, player.boxCastSize, 0, Vector2.zero, 0, mask);
        Color c = new Color();
        if (hit.collider != null)
        {
            c = Color.green;
            isGounded = true;
        }
        else
        {
            c = Color.gray;
            isGounded = false;
        }
    }
}
