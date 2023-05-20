using UnityEngine;

public class ClimbState : Istate
{
    Player player;
    float moveUpSpeed = 2;
    public ClimbState(Player player)
    {
        this.player = player;
    }

    bool climbAnim = false;//prevent climb anim over call
    //how should i seprate climb anim from climb to stand anim
    //set this anim when player middle is crossed the leader top edge 
    //~first step , just play climbstand on leader climb up 
    //the leader end will be zero and the leader climb end point will be 1 and currnet value should be a between zero and one and be applied to climb anim time 
    public void OnEnter()
    {
        player.GetCurrentLeaderInfo();
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
        Debug.Log(climbAnim);
    }

    void AnimationHandler()
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
            if (player.transform.position.y >= player.leaderClimbEndPoint)
                player.stateManager.Transition(player.stateManager.idle);
        if (Input.GetKey(KeyCode.S))
            if (player.transform.position.y <= player.leaderClimbStartPoint)
                player.stateManager.Transition(player.stateManager.idle);
    }

}
