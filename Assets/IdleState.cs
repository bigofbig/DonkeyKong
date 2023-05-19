using UnityEngine;

public class IdleState : Istate
{
    Player player;
    public IdleState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {
        player.rb.velocity = Vector2.zero;
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

        if (Input.GetKey(KeyCode.W) && player.climbState == Player.ClimbStates.CanClimbUp)
            player.stateManager.Transition(player.stateManager.climb);
        if (Input.GetKey(KeyCode.S) && player.climbState == Player.ClimbStates.CanClimbDown)
            player.stateManager.Transition(player.stateManager.climb);
        //how to seprate going down form going upg
    }
}
