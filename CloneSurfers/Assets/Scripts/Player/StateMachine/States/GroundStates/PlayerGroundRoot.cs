using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundRoot : PlayerBaseState
{
    PlayerStateManager _stateManager;
    PlayerGroundIdleState _groundIdleState;
    PlayerGroundRunState _groundRunState;

    PlayerController.States _thisEnumState = PlayerController.States.Ground;

    bool _onSlope;
    public PlayerGroundRoot(PlayerController controller) : base(controller) { }

    public override void EnterState(PlayerStateManager stateManager)
    {
        _onSlope = false;
        _controller.SwitchColliders(_controller.StandCollider);

        if (_stateManager == null)
        {
            _stateManager = stateManager;
            InitializeChildren();
        }
        if (!_controller.IsOnGround())
        {
            _controller.RB.useGravity = true;
            _controller.RB.AddForce(new Vector3(0, -800, 0));
        }
        _controller.CurrentEnumState = _thisEnumState;
    }
    void InitializeChildren()
    {
        _groundIdleState = new PlayerGroundIdleState(_controller);
        _groundRunState = new PlayerGroundRunState(_controller);
    }

    public override void UpdateState()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            _controller.NextCommand();
            _controller.TryToSwitchState(_groundIdleState, _thisEnumState);
            return;
        }
        else
            _controller.TryToSwitchState(_groundRunState, _thisEnumState);
        if (_controller.IsOnSlope())
            _onSlope = true;

        if (!_controller.IsOnGround())
        {
            _controller.RB.useGravity = true;
            if (_onSlope)
            {
                _controller.RB.AddForce(new Vector3(0, -400, 0));
                _onSlope = false;
            }
        }
        else
            _controller.RB.useGravity = false;

        string command = _controller.NextCommand();
        if (command == "Jump")
            _controller.TryToSwitchState(_controller._jumpRoot, _thisEnumState);
        else if (command == "Slide")
            _controller.TryToSwitchState(_controller._slideRoot, _thisEnumState);


    }

    public override void ExitState()
    {

    }

}
