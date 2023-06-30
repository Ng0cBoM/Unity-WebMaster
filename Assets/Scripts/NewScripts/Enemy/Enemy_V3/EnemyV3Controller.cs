using EgdFoundation;
using UnityEngine;

public class EnemyV3Controller : MonoBehaviour
{
    Fsm<EnemyV3Controller> stateMachine;

    private void Start()
    {
        stateMachine = new Fsm<EnemyV3Controller>(this);

        stateMachine.AddState<HaveOverState>();
        stateMachine.AddState<HaventOverState>();

        stateMachine.SwitchState<HaveOverState>();
    }



}
