using EgdFoundation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : State<PlayerController2>
{
    public override void Enter()
    {
        base.Enter();
        _owner.rb.drag = 1;
        _owner.ninjaWeapon.SetActive(false);
        _owner.line.SetActive(false);
        _owner.PlayAnim("Hurt");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
