public class DeadState : Istate
{
    public Player player;
    public DeadState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.rb.bodyType = UnityEngine.RigidbodyType2D.Static;
        player.animator.Play(player.deadStandAnim);
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
