public interface Istate
{
    public void OnEnter();
    public void OnUpdate();
    public void OnFixedUpdate();
    public void OnExit();
}

public interface IHammerable
{
    public void OnHammered();
}
