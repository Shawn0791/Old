﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprouterIdleBehaviour : StateMachineBehaviour
{
    private Transform player;
    private Transform sprouter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprouter = animator.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<Sprouter>().findTarget == true)
        {
            Turn();

            if (Vector2.Distance(player.transform.position, animator.transform.position) <= 2)
            {
                animator.SetTrigger("attack");
            }
            else
            {
                animator.SetBool("moving", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //转向
    void Turn()
    {
        if (player.transform.position.x > sprouter.position.x)
        {
            sprouter.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            sprouter.localScale = new Vector3(-1, 1, 1);
        }
    }
}
