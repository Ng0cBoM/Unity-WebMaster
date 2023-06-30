using EgdFoundation;
public class PlayerFallingState : State<PlayerController2>
{
    public override void Enter()
    {
        base.Enter();
        _owner.PlayAnim("Falling");
        _owner.rb.isKinematic = false;
        _owner.isStand = false;
    }

    public override void Update()
    {
        base.Update();
        if (_owner.isMoveWeapon)
        {
            _owner.MoveNinjaWeapon();
        }
        if (_owner.isPullTheRope)
        {
            _owner.PullTheRope();
        }
        if (_owner.isStand)
        {
            _owner.targetTag = "AboveWall";
            _fsm.SwitchState<PlayerStandState>();
        }
    }
}
