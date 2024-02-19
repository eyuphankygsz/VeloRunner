using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpUpState : PlayerBaseState
{
    public PlayerJumpUpState(PlayerController controller) : base(controller){}

    PlayerStateManager _stateManager;
    public override void EnterState(PlayerStateManager stateManager)
    {
        _controller.Anim.SetTrigger("Jump");
        _controller.SwitchColliders(_controller.StandCollider);

        if (_stateManager == null)
        {
            _stateManager = stateManager;
        }
        _controller.RB.useGravity = true;
        _controller.RB.velocity = Vector3.zero;
        _controller.RB.AddForce(new Vector3(0, 300, 0));
    }
    public override void UpdateState()
    {
        _controller._jumpRoot.UpdateState();
    }

    public override void ExitState()
    {
        _controller.Anim.ResetTrigger("Jump");
    }

}
