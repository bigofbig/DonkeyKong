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

        if (Input.GetKey(KeyCode.Space) )
            player.stateManager.Transition(player.stateManager.jump);
    }
}
