using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundRunState : PlayerBaseState
{
    public PlayerGroundRunState(PlayerController controller) : base(controller){}

    PlayerStateManager _stateManager;
    public override void EnterState(PlayerStateManager stateManager)
    {
        _controller.Anim.SetTrigger("Walk");
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

        _controller.Anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }
    public override void ExitState()
    {
        _controller.Anim.ResetTrigger("Walk");
    }
}
