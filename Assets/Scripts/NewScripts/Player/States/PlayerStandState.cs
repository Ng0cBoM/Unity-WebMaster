using EgdFoundation;

public class PlayerStandState : State<PlayerController2>
{
    public override void Enter()
    {
        base.Enter();
        _owner.rb.isKinematic = true;
        _owner.hand.transform.position = _owner.transform.position;
        _owner.line.SetActive(false);
        _owner.ninjaWeapon.SetActive(false);
        _owner.lineRenderer.SetPosition(1, _owner.transform.position);
        _owner.ninjaWeapon.transform.position = _owner.transform.position;
        if (_owner.targetTag == "RightWall")
        {
            _owner.PlayAnim("Hangright");
        }
        if (_owner.targetTag == "LeftWall")
        {
            _owner.PlayAnim("Hangright");
        }
        if (_owner.targetTag == "UnderWall")
        {
            _owner.PlayAnim("Hang");
        }
        if (_owner.targetTag == "AboveWall")
        {
            _owner.PlayAnim("Idle");
        }
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

    }
}
