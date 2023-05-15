using UnityEngine;

public class RunState : Istate
{
    Player player;
    bool playerShouldFaceRight;
    float speed = 2;
    public RunState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {
        playerShouldFaceRight = Input.GetKey(KeyCode.D);
        player.SetFaceing(playerShouldFaceRight);
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        //if user holding nothing ,switch to idle state
        if (playerShouldFaceRight)
            player.rb.velocity = new Vector2(1 * player.runSpeed, player.rb.velocity.y);
        else
            player.rb.velocity = new Vector2(-1 * player.runSpeed, player.rb.velocity.y);

    }

    public void OnUpdate()
    {
        if (playerShouldFaceRight && !Input.GetKey(KeyCode.D))
        { player.stateManager.Transition(player.stateManager.idle); return; }
        else if (!playerShouldFaceRight && !Input.GetKey(KeyCode.A))
        { player.stateManager.Transition(player.stateManager.idle); return; }

        if (Input.GetKeyDown(KeyCode.Space))
            player.stateManager.Transition(player.stateManager.jump);

    }
}
