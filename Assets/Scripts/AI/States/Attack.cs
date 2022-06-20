using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    CharacterStateMachine stateMachine;

    [SerializeField]
    float attackTime;

    Timer attackTimer;
    Transform playerrTr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void UpdateState()
    {
        if( attackTimer.CheckTimer(Time.deltaTime ) )
        {
            ShouldExit = true;
           



        }
        else
        {
            float distance = Vector3.Distance( transform.position, playerrTr.position );
            transform.LookAt( playerrTr, Vector3.up );
            if ( distance > 2 )
            {
                stateMachine.MoveComponent.SetDestination( playerrTr.position );
            }
            else
            {
                stateMachine.MoveComponent.SetDestination( transform.position );
            }
        }
       /* if ( !ShouldExit )
        {
            activable?.Activate();
            ShouldExit = true;
        }*/

    }
    public override void OnStateExit( StateMachine stateMachine_ )
    {
        stateMachine.Animator.SetBool( "Attack", false );
    }
    public override void OnStateEnter( StateMachine stateMachine_ )
    {
        playerrTr = FindObjectOfType<PlayerStateMachine>().transform;
        stateMachine = stateMachine_ as CharacterStateMachine;
        stateMachine.Animator.SetBool( "Attack", true );
        ShouldExit = false;
        attackTimer = new Timer( attackTime );

    }
}
