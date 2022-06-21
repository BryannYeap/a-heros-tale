using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWalk1 : StateMachineBehaviour
{
    public float speed;
    public float timer;
    public float minTime;
    public float maxTime;

    Transform target;
    Transform left;
    Transform right;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);

        left = GameObject.Find("left").transform;
        right = GameObject.Find("right").transform;

        target = Random.Range(0, 2) == 0 ? left : right;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("attack");
        } else
        {
            timer -= Time.deltaTime;
        }

        Vector2 destination = new Vector2(target.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, destination, speed * Time.fixedDeltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
