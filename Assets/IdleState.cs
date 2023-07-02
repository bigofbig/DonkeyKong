using UnityEngine;

public class IdleState : Istate
{
    Player player;
    float minimumDistanceToClimbALadder = .5f;
    LayerMask mask = LayerMask.GetMask("Ground");
    bool isGounded = false;
    public IdleState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {
        if (player.transform.position.y > 6)
        {
            GameEvents.current.CallOnWin();
            player.stateManager.Transition(player.stateManager.win);
            return;
        }
        player.rb.velocity = Vector2.zero;
        if (player.stateManager.lastState == player.stateManager.jump)
            player.animator.Play(player.landToIdleAnim);
        else
        {
            //we have been here
            //Debug.Log("idle anim play");
            player.animator.Play(player.idleAnim);
        }
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (player.isHammerTime)
            player.stateManager.Transition(player.stateManager.hammerIdle);

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
            player.stateManager.Transition(player.stateManager.run);

        if (Input.GetKey(KeyCode.Space))
            player.stateManager.Transition(player.stateManager.jump);

        //set condiotion for distance from ladder 
        if (Input.GetKey(KeyCode.W) && player.climbState == Player.ClimbStates.CanClimbUp)
            if (IsPlayerCloseEnough())
                player.stateManager.Transition(player.stateManager.climb);
        if (Input.GetKey(KeyCode.S) && player.climbState == Player.ClimbStates.CanClimbDown)
            if (IsPlayerCloseEnough())
                player.stateManager.Transition(player.stateManager.climb);
    }

    bool IsPlayerCloseEnough()
    {
        float distance = player.transform.position.x - player.currentLeader.transform.position.x;
        return distance <= minimumDistanceToClimbALadder;
    }
}
