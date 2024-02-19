using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundIdleState : PlayerBaseState
{
    public PlayerGroundIdleState(PlayerController controller) : base(controller){}

    PlayerStateManager _stateManager;
    public override void EnterState(PlayerStateManager stateManager)
    {
        if (_stateManager == null)
        {
            _stateManager = stateManager;
        }
        _controller.RB.useGravity = false;
    }
    public override void UpdateState()
    {
        _controller._groundRoot.UpdateState();
        if (_stateManager.CurrentState != this)
            return;

        _controller.Anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    public override void ExitState()
    {

    }
}
