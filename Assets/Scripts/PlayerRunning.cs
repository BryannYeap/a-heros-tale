using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : StateMachineBehaviour
{
    private const float timeInterval = 0.2f;

    private bool running = false;
    private float timer = timeInterval;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        running = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (running)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } else
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().m_Grounded) FindObjectOfType<AudioManager>().Play("Walk");
                timer = timeInterval;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        running = false;
    }

}
