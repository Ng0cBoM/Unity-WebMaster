using System;
using EgdFoundation;
using UnityEngine;

public class EnemyGreet: State<ExampleEntity>
{
    public override void Enter()
    {
        base.Enter();
        _owner.SayHiAndBackToGuard();
    }
}
