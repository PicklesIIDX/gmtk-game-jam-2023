using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waddle : StateMachineBehaviour
{
	private Mover _mover;
	private GameObject _hero;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_mover = animator.gameObject.GetComponent<Mover>();
		_hero = PositionGetter.FindPlayer();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (_mover.Move())
		{
			animator.SetBool("reachedPosition", true);
		}
		if (_hero)
		{
			var distanceToHero = Vector2.Distance(_hero.transform.position, _mover.transform.position);
			animator.SetFloat("distanceToHero", distanceToHero);
		}
		else
		{
			animator.SetFloat("distanceToHero", 99);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_mover.Stop();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
