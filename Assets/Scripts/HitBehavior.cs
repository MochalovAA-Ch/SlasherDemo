using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehavior : StateMachineBehaviour
{
    [SerializeField]
    [Tooltip( "¬рем€ с начала анимации, после которого можно продолжить комбинацию" )]
    float comboTime;
    [SerializeField]
    [Tooltip( "¬рем€  с начала анимации, с которого начнетс€ засчитывание удара" )]
    float startTimeHitActive;
    [SerializeField]
    [Tooltip( "¬рем€  с начала анимации, с которого заканчиваетс€ засчитывание удара" )]
    float endTimeHitActive;

    [SerializeField]
    string stateParamName;
    [SerializeField]
    string nextStateParamName;



    float timer;
    bool wasClicked = false;
    bool canHitNextCombo = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool( stateParamName, true );
        timer = 0.0f;
        wasClicked = false;
        canHitNextCombo = true;
        //Global.isSwordInHitState = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        if ( timer >= stateInfo.length )
        {
            animator.SetBool( stateParamName, false );
            Global.isSwordInHitState = false;
            return;
            //Debug.Log( "OLOLOL" );
        }

        if ( timer > startTimeHitActive && timer < endTimeHitActive )
            Global.isSwordInHitState = true;
        else if ( timer > endTimeHitActive )
            Global.isSwordInHitState = false;
        //≈сли был клик мыши во врем€ проигрывани€ анимации
        if ( !wasClicked )
        {
            wasClicked = animator.GetBool( "Click" );
        }
        
        if( wasClicked && canHitNextCombo )
        {
            //ѕопали в промежуток дл€ комбо
            if ( timer >= comboTime )
            {   if(nextStateParamName != "" )
                    animator.SetBool( nextStateParamName, true );
                else
                    animator.SetBool( stateParamName, false );
            }
            else
            {
                canHitNextCombo = false;
            }
        }

        timer += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Global.isSwordInHitState = false;
        animator.SetBool( stateParamName, false );
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
