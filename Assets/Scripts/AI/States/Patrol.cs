using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float walkPointRange;
    [SerializeField]
    float breakTime;

    Timer breakTimer;


    Vector3 walkPoint;
    bool walkPointSet;

    CharacterStateMachine stateMachine;

    public LayerMask whatIsGround, whatIsPlayer;

    public override void UpdateState() 
    {
        //agent.speed = walkSpeed;
        if ( !walkPointSet )
        {
            if( breakTimer.CheckTimer( Time.deltaTime ) )
                SearchWalkPoint();
        }
        else
            stateMachine.MoveComponent.SetDestination( walkPoint );

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if ( distanceToWalkPoint.magnitude < 1f )
        {
            walkPointSet = false;
            //SetAnimState( EnemyAnimStates.IDLE );
        }
        else
        {
            //SetAnimState( EnemyAnimStates.PATROL );
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range( -walkPointRange, walkPointRange );
        float randomX = Random.Range( -walkPointRange, walkPointRange );

        walkPoint = new Vector3( transform.position.x + randomX, transform.position.y, transform.position.z + randomZ );

        if ( Physics.Raycast( walkPoint, -transform.up, 2f, whatIsGround ) )
            walkPointSet = true;

        if ( walkPointSet )
            breakTimer.ResetTimer();
    }


    public override void OnStateExit( StateMachine stateMachine ) { }
    public override void OnStateEnter( StateMachine stateMachine_ ) 
    {
        breakTimer = new Timer( breakTime );
        stateMachine = stateMachine_ as CharacterStateMachine;
        stateMachine.MoveComponent.SetSpeed( walkSpeed );
    }

    private void OnDrawGizmosSelected()
    {
        //if( this.Is)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position, walkPointRange );
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere( transform.position, sightRange );
    }
}
