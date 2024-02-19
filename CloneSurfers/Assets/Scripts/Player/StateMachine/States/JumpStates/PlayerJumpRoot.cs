using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpRoot : PlayerBaseState
{
    PlayerStateManager _stateManager;

    PlayerController.States _thisEnumState = PlayerController.States.Jump;

    PlayerJumpUpState _jumpUpState;

    bool _groundCheck;

    float _groundCheckTimer, _defaultGroundCheckTime = 0.2f;

    public PlayerJumpRoot(PlayerController controller) : base(controller) { }

    public override void EnterState(PlayerStateManager stateManager)
    {
        _groundCheckTimer = _defaultGroundCheckTime;
        _groundCheck = false;
        if (_stateManager == null)
        {
            _stateManager = stateManager;
            InitializeChildren();
        }
        _controller.RB.useGravity = true;
        _controller.CurrentEnumState = _thisEnumState;
        _controller.TryToSwitchState(_jumpUpState, _thisEnumState);
    }
    void InitializeChildren()
    {
        _jumpUpState = new PlayerJumpUpState(_controller);
    }

    public override void UpdateState()
    {

        if (_groundCheck)
            if (_controller.IsOnGround())
                _controller.TryToSwitchState(_controller._groundRoot, _thisEnumState);

        CheckGroundWait();
        string command = _controller.NextCommand();

        if (command == "Slide")
            _controller.TryToSwitchState(_controller._slideRoot, _thisEnumState);
    }
    void CheckGroundWait()
    {
        if (_groundCheckTimer <= 0)
            _groundCheck = true;
        else
            _groundCheckTimer -= Time.deltaTime;
    }
    public override void ExitState()
    {

    }

}
