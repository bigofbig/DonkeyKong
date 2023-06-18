using System;

[Serializable]
public class StateManager
{
    public Istate currentState { get; private set; }
    public Istate lastState { get; private set; }

    public IdleState idle;
    public RunState run;
    public JumpState jump;
    public ClimbState climb;
    public DeadState dead;
    public HammerIdleState hammerIdle;
    public HammerRunState hammerRun;
    //win State

    public Action OnStateChanged;

    public StateManager(Player player)
    {
        idle = new IdleState(player);
        run = new RunState(player);
        jump = new JumpState(player);
        climb = new ClimbState(player);
        dead = new DeadState(player);
        hammerIdle = new HammerIdleState(player);
    }

    public void Initialize(Istate startingState)
    {
        currentState = startingState;
        startingState.OnEnter();
    }
    public void Transition(Istate targetState)
    {
        currentState.OnExit();
        lastState = currentState;
        currentState = targetState;
        currentState.OnEnter();

        OnStateChanged?.Invoke();
    }
    public void Update()
    {
        if (currentState == null) return;
        currentState.OnUpdate();
    }
    public void FixedUpdate()
    {
        if (currentState == null) return;
        currentState.OnFixedUpdate();
    }
}
