using UnityEngine;

public class ClimbState : Istate
{
    Player player;
    //disable rigidbody
    //set player X pos at center of leader
    //at the end rigidBody
    //top y pos
    //down y pos
    float moveUpSpeed = 2;
    public ClimbState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.GetCurrentLeaderInfo();
        player.rb.isKinematic = true;
        player.transform.position = new Vector3(player.leaderXPos, player.transform.position.y);
    }

    public void OnExit()
    {
        player.rb.isKinematic = false;
        player.SetClimbState();
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        TransitionOnLeaderEnd();
        InputHandler();
        //Debug.Log(player.transform.position.y + " --> " + player.leaderStartYValue);
    }

    void InputHandler()
    {
        if (Input.GetKey(KeyCode.W))
            player.transform.position += (Vector3)Vector2.up * moveUpSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            player.transform.position -= (Vector3)Vector2.up * moveUpSpeed * Time.deltaTime;
    }

    void TransitionOnLeaderEnd()
    {
        if (Input.GetKey(KeyCode.W))
            if (player.transform.position.y >= player.leaderEndYValue)
                player.stateManager.Transition(player.stateManager.idle);
        if (Input.GetKey(KeyCode.S))
            if (player.transform.position.y <= player.leaderStartYValue)
                player.stateManager.Transition(player.stateManager.idle);
    }

}
