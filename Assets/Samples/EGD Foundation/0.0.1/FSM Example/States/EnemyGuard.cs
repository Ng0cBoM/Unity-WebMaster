using EgdFoundation;
using UnityEngine;

public class EnemyGuard : State<ExampleEntity>
{
    float guardPhaseDuration = 8f;
    float currentPhaseTime;
    public override void Enter()
    {
        base.Enter();
        _owner.PlayAnim("Walking");
        currentPhaseTime = guardPhaseDuration;  
    }

    public override void Update()
    {
        base.Update();
        
        if (currentPhaseTime <= 0)
        {
            _owner.TurnAround();
            currentPhaseTime = guardPhaseDuration;
        }
        else
        {
            currentPhaseTime -= Time.deltaTime;
        }

        _owner.Walk();

        if (_owner.needToGreet && _owner.IsNearPlayer())
        {
            _fsm.SwitchState<EnemyGreet>();
        }
    }
}
