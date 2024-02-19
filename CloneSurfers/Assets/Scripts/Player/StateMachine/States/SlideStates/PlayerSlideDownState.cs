using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideDownState : PlayerBaseState
{
    public PlayerSlideDownState(PlayerController controller) : base(controller) { }

    PlayerStateManager _stateManager;
    bool _onSlope;
    public override void EnterState(PlayerStateManager stateManager)
    {
        _onSlope = false;
        _controller.Anim.SetTrigger("Slide");
        _controller.SwitchColliders(_controller.SlideCollider);
        if (_stateManager == null)
            _stateManager = stateManager;

        if (!_controller.IsOnGround())
        {
            _controller.RB.useGravity = true;
            _controller.RB.AddForce(new Vector3(0, -800, 0));
        }
    }
    public override void UpdateState()
    {
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


        _controller._slideRoot.UpdateState();
        if (_stateManager.CurrentState != this)
            return;


    }
    public override void ExitState()
    {
        _controller.Anim.ResetTrigger("Slide");
    }
}
