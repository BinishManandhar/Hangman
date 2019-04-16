using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectStateBehaviours : StateMachineBehaviour
{
    public BehaviourScript behaviourScript;
    Button button;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ButtonToggle(false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ButtonToggle(true);
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

    private void ButtonToggle(bool toggle) {
        button = GameObject.FindGameObjectWithTag("CheckButton").GetComponent<Button>();
        GameObject.Find("option(1)").GetComponent<Button>().enabled = toggle;
        GameObject.Find("option(2)").GetComponent<Button>().enabled = toggle;
        GameObject.Find("option(3)").GetComponent<Button>().enabled = toggle;
        GameObject.Find("option(4)").GetComponent<Button>().enabled = toggle;
        button.enabled = toggle;
    }
}
