using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPosition : StateMachineBehaviour
{
    public enum FindType
    {
        None,
        TargetHero,
        AvoidHero,
    }

    [SerializeField] private FindType _findType = FindType.TargetHero;
    [SerializeField] private float _maxHuntRange = 8.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var mover = animator.GetComponent<Mover>();
        var position = mover.transform.position;
        switch (_findType)
        {
            case FindType.TargetHero:
                position = PositionGetter.FindNewPositionNearHeroWithinRange(mover.transform.position, _maxHuntRange);
                break;
            case FindType.AvoidHero:
                position = PositionGetter.FindPositionAwayFromHero(mover.transform.position, 6f);
                break;
        }
        mover.TargetPosition = position;
        animator.SetBool("hasPosition", true);
        animator.SetFloat("distanceToHero", 99);
        animator.SetBool("reachedPosition", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
