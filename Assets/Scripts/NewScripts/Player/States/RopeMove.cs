using EgdFoundation;
using System.Diagnostics;

public class RopeMove : State<PlayerController2>
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        _owner.MoveNinjaWeapon();
        
    }
}
