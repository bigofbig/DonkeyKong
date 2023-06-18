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
        //play anim
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

        if (!isGounded) return;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            player.stateManager.Transition(player.stateManager.hammerRun);

        //sphere cast 

    }
}
