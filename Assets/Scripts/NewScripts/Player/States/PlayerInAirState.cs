using EgdFoundation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInAirState : State<PlayerController2>
{
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        _owner.rb.isKinematic = true;
        Flip();
        _owner.particleInAir.SetActive(true);
        PlayAnimFly();
        _owner.isFlying = true;
    }
    void PlayAnimFly()
    {
        if (_owner.targetPosition.y < _owner.transform.position.y)
            _owner.PlayAnim("FlyDown");
        else 
            _owner.PlayAnim("Fly");
    }

    void Flip()
    {
        float scaleX = 1;
        if (_owner.targetPosition.x < _owner.transform.position.x) scaleX = -1;
        float scaleY = _owner.transform.localScale.y;
        float scaleZ = _owner.transform.localScale.z;
        _owner.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    public override void Update()
    {
        base.Update();
        if (_owner.isMouseButtonDown)
        {
            _owner.line.SetActive(false);
            _owner.ninjaWeapon.SetActive(false);
            _fsm.SwitchState<PlayerFallingState>();
        }
        _owner.MoveCharactor();
    }
    public override void Exit()
    {
        base.Exit();
        _owner.particleInAir.SetActive(false); 
        _owner.isFlying = false;

    }
}
