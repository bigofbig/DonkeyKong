public class DeadState : Istate
{
    public Player player;
    public DeadState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {

        AudioManager.current.Play("Death");
        player.rb.bodyType = UnityEngine.RigidbodyType2D.Static;
        player.animator.Play(player.deadStandAnim);
        GameEvents.current.CallGameOver();
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
    }
}
