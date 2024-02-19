using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideRoot : PlayerBaseState
{
    PlayerStateManager _stateManager;

    PlayerController.States _thisEnumState = PlayerController.States.Slide;

    PlayerSlideDownState _slideDownState;

    float _slideTimer, _defaultSlideTime = 0.8f;

    public PlayerSlideRoot(PlayerController controller) : base(controller) { }

    public override void EnterState(PlayerStateManager stateManager)
    {
        if (_stateManager == null)
        {
            _stateManager = stateManager;
            InitializeChildren();
        }
        _controller.CurrentEnumState = _thisEnumState;
        _controller.TryToSwitchState(_slideDownState, _thisEnumState);
    }
    void InitializeChildren()
    {
        _slideDownState = new PlayerSlideDownState(_controller);
    }

    public override void UpdateState()
    {
        _slideTimer -= Time.deltaTime;
        if (_slideTimer <= 0)
            _controller.TryToSwitchState(_controller._groundRoot, _thisEnumState);

        string command = _controller.NextCommand();
        if (command == "Jump")
            _controller.TryToSwitchState(_controller._jumpRoot, _thisEnumState);
    }
    public override void ExitState()
    {
        _slideTimer = _defaultSlideTime;
    }

}

