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
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (player.transform.position.y >= player.leaderEndYValue)
        {
            Debug.Log("leader finished from up! ");
            player.stateManager.Transition(player.stateManager.idle);
            return;
        }
        if (player.transform.position.y <= player.leaderStartYValue)
        {
            Debug.Log("leader finished from down! ");
            player.stateManager.Transition(player.stateManager.idle);
            return;
        }
        if (Input.GetKey(KeyCode.W))
            player.transform.position += (Vector3)Vector2.up * moveUpSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            player.transform.position -= (Vector3)Vector2.up * moveUpSpeed * Time.deltaTime;
    }


}
