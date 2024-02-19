using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState _currentState;
    public PlayerBaseState CurrentState { get { return _currentState; } }
    
    PlayerController _controller;
    public PlayerController Controller { get { return _controller;} }
    
    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }
    public void SwitchState(PlayerBaseState newState)
    {
        if (_currentState != null)
            _currentState.ExitState();

        _currentState = newState;
        _currentState.EnterState(this);
    }
}
