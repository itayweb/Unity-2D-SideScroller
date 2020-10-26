using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonIdle : StateMachineBehaviour
{
    public LayerMask playerDetection;
    Transform transform;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.GetComponent<Transform>();       
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, 6f, playerDetection))
        {
            animator.SetTrigger("Chase");
        }
        else if (Physics2D.Raycast(transform.position, Vector2.right, 6f, playerDetection))
        {
            animator.SetTrigger("Chase");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Chase");
    }
}
