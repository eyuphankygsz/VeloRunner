using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitRoot : PlayerBaseState
{
    public PlayerHitRoot(PlayerController controller) : base(controller)
    {
    }

    Color _defaultBodyColor;
    Color _defaultHairColor;

    bool _firstTry = false;
    Color _red = Color.red;
    public override void EnterState(PlayerStateManager stateManager)
    {

    }
    public void StartHit()
    {
        if (!_firstTry)
        {
            _firstTry = true;
            _defaultBodyColor = _controller.BodyMaterial.color;
            _defaultHairColor = _controller.HairMaterial.color;
        }
    }
    public void ReturnToNormal()
    {
        _controller.BodyMaterial.color = _defaultBodyColor;
        _controller.HairMaterial.color = _defaultHairColor;
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        _controller.BodyMaterial.color = Color.Lerp(Color.white, _red, Mathf.PingPong(Time.time, 1f));
        _controller.HairMaterial.color = Color.Lerp(Color.white, _red, Mathf.PingPong(Time.time, 1f));
    }
}
