using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EgdFoundation;

public class ExampleEntity : MonoBehaviour
{
	[HideInInspector]
    public Vector3 moveDirection = Vector3.forward;

    [HideInInspector]
    public bool needToGreet { private set; get; } = true;

    [SerializeField] Transform playerDemo;
	[SerializeField] float detectingRange = 5;
	[SerializeField] float walkSpeed = 5;
	[SerializeField] float turnSpeed = 180;
	[SerializeField] float greetingTime = 5f;
    [SerializeField] float noGreetingTime = 10f;

    Fsm<ExampleEntity> stateMachine;
	Animator animator;

	

	
	void Start()
	{
		animator = GetComponent<Animator>();

		//init FSM
		stateMachine = new Fsm<ExampleEntity>(this);
		stateMachine.AddState<EnemyGuard>();
		stateMachine.AddState<EnemyGreet>();

		stateMachine.SwitchState<EnemyGuard>();	
	}

	void Update()
	{
		//state logics places in state class, ex: EnemyGuard.
		stateMachine.Update();

		
    }

	public bool IsNearPlayer()
	{
		return Vector3.Distance(transform.position, playerDemo.position) < detectingRange;
	}

	public void SayHiAndBackToGuard()
	{
		this.PlayAnim("Greeting");
		transform.LookAt(playerDemo);
		needToGreet = false;
		StartCoroutine(BackToGuard());
		
	}

	IEnumerator BackToGuard()
	{
		yield return new WaitForSeconds(greetingTime);
		stateMachine.SwitchState<EnemyGuard>();
        StartCoroutine(NoGreeting());
    }

    IEnumerator NoGreeting()
    {
        yield return new WaitForSeconds(noGreetingTime);
		needToGreet = true;
    }

    public void Walk()
	{
		transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
        //face direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, turnSpeed * Time.deltaTime);
        }
    }

	public void PlayAnim(string animName)
	{
		animator.CrossFade(animName, 0.5f);
	}

	public void TurnAround()
	{
		//moveDirection = new Vector3(Random.Range(-1, 1), 0, 0);
		moveDirection *= -1;
    }
}

