using UnityEngine;

public class IdleState : Istate
{
    Player player;
    float minimumDistanceToClimbALadder = .5f;
    public IdleState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {
        player.rb.velocity = Vector2.zero;
        if (player.stateManager.lastState == player.stateManager.jump)
            player.animator.Play(player.landToIdleAnim);
        else
            player.animator.Play(player.idleAnim);
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
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
