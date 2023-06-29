using UnityEngine;

public class WinState : Istate
{
    Player player;
    bool idleAnimNotSet = true;
    public WinState(Player player)
    {
        this.player = player;
    }
    public void OnEnter()
    {
        player.rb.bodyType = RigidbodyType2D.Static;
        player.transform.position = new Vector3(1.83f, 7.06f);
        player.SetFaceing(false);
    }

    public void OnExit()
    {
    }
    public void OnFixedUpdate()
    {
        if (idleAnimNotSet)
        {
            //this is becuase of that unkown bug...
            player.animator.Play(player.idleAnim);
            idleAnimNotSet = false;
        }

    }
    public void OnUpdate()
    {
    }

}
