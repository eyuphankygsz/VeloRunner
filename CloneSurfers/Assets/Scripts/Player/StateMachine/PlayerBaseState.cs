using UnityEngine;

public abstract class PlayerBaseState
{
    public PlayerController _controller { get; private set; }
    public PlayerBaseState(PlayerController controller)
    {
        _controller = controller;
    }
    public abstract void EnterState(PlayerStateManager stateManager);
    public abstract void UpdateState();
    public abstract void ExitState();

}
