using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : State
{
    CharacterStateMachine stateMachine;

    [SerializeField]
    float minDistanceToChase;
    [SerializeField]
    float speed;

    Transform player;

    public override void UpdateState() 
    {
        float distance = Vector3.Distance( transform.position, player.position );
        if ( distance > minDistanceToChase )
        {
            stateMachine.MoveComponent.SetDestination( player.position );
        }
        else
        {
            ShouldExit = true;
            //SetAnimState( EnemyAnimStates.IDLE );
            //stateMachine.MoveComponent.SetDestination( transform.position );
        }
    }

    public override void OnStateExit( StateMachine stateMachine_ ) 
    {
        

    }
    public override void OnStateEnter( StateMachine stateMachine_ ) 
    {
        player = GameObject.FindObjectOfType<PlayerStateMachine>().transform;
        ShouldExit = false;
        stateMachine = stateMachine_ as CharacterStateMachine;
        stateMachine.MoveComponent.SetSpeed( speed );
    }
}
