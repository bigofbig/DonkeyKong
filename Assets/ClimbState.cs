using UnityEngine;

public class ClimbState : Istate
{
    Player player;
    float moveUpSpeed = 2;
    bool climbAnim = false;
    public ClimbState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.GetCurrentLeaderInfo();
        player.rb.velocity = Vector2.zero;
        player.rb.isKinematic = true;
        player.transform.position = new Vector3(player.leaderXPos, player.transform.position.y);
        player.animator.Play(player.climbAnim);
        climbAnim = true;
    }
    public void OnExit()
    {
        player.rb.isKinematic = false;
        player.SetClimbState();
        player.animator.speed = 1;
    }
    public void OnFixedUpdate()
    {
    }
    public void OnUpdate()
    {
        TransitionOnLeaderEnd();
        InputHandler();
        AnimationHandler();
    }

    void AnimationHandler()
    {
        if (player.thisLeaderIsbroken)
        {
            if (!climbAnim)
            {
                climbAnim = true;
                player.animator.Play(player.climbAnim);
            }

            if ((Input.GetKey(KeyCode.W) && player.transform.position.y < player.leaderClimbEndPoint) || Input.GetKey(KeyCode.S))
                player.animator.speed = 1;
            else
                player.animator.speed = 0;

        }
        else
        {
            if (player.transform.position.y >= player.leaderEnd)
            {
                float currnetValue = player.transform.position.y;
                float begin = player.leaderEnd;
                float end = player.leaderClimbEndPoint;
                float normalized = Mathf.InverseLerp(begin, end, currnetValue);

                if (climbAnim)
                    climbAnim = false;
                player.animator.Play(player.climStandAnim, 0, normalized);
                //Mathf.InverseLerp(begin, end, currnetValue);
                //end- begin= diffrance
            }
            else
            {
                if (!climbAnim)
                {
                    climbAnim = true;
                    player.animator.Play(player.climbAnim);
                }

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                    player.animator.speed = 1;
                else
                    player.animator.speed = 0;
            }
        }
    }
    void InputHandler()
    {
        //going up
        switch (player.thisLeaderIsbroken)
        {
            case false:
                if (Input.GetKey(KeyCode.W))
                    player.transform.position += (Vector3)Vector2.up * moveUpSpeed * Time.deltaTime;
                break;

            case true:
                bool canStealClimbUp = player.transform.position.y < player.leaderClimbEndPoint;
                if (canStealClimbUp && Input.GetKey(KeyCode.W))
                    player.transform.position += (Vector3)Vector2.up * moveUpSpeed * Time.deltaTime;
                break;
        }
        //going down
        if (Input.GetKey(KeyCode.S))
            player.transform.position -= (Vector3)Vector2.up * moveUpSpeed * Time.deltaTime;
    }
    void TransitionOnLeaderEnd()
    {

        if (Input.GetKey(KeyCode.S))
            if (player.transform.position.y <= player.leaderClimbStartPoint)
                player.stateManager.Transition(player.stateManager.idle);

        if (player.thisLeaderIsbroken) return;
        if (Input.GetKey(KeyCode.W))
            if (player.transform.position.y >= player.leaderClimbEndPoint)
                player.stateManager.Transition(player.stateManager.idle);
    }

}
