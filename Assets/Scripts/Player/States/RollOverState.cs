using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollOverState : State
{
    [SerializeField]
    float StartMoveSpeed;

    [SerializeField]
    float averageMoveSpeed;

    float speed;

    float ySpeed;
    Vector3 direction;

    Timer timer = new Timer( 0.8f );

    CharacterStateMachine stateMachine;
    public override void OnStateEnter( StateMachine stateMachine_ )
    {
        ShouldExit = false;
        this.stateMachine = stateMachine_ as CharacterStateMachine;
        ySpeed = -9.8f;
        float x = Input.GetAxis( "Horizontal" );
        float z = Input.GetAxis( "Vertical" );
        direction = new Vector3( x, 0, z );

        direction = Quaternion.AngleAxis( stateMachine.GetCameraRotationEulers().y, Vector3.up ) * direction;
        direction.Normalize();

        stateMachine.Animator.SetBool("RollOver", true);
        stateMachine.MoveComponent.MultiplyHeght(0.5f);
        speed = StartMoveSpeed;
        timer.ResetTimer();
    }

    public override void OnStateExit( StateMachine stateMachine )
    {
        this.stateMachine.Animator.SetBool("RollOver", false);
        this.stateMachine.MoveComponent.SetDefaultHeight();
    }

    public override void UpdateState()
    {
        if( timer.CheckTimer( Time.deltaTime ) )
        {
            ShouldExit = true;
            return;
        }

        if ( stateMachine.MoveComponent.IsGrounded() )
        {
            ySpeed = -stateMachine.CharacterData.Gravity;
        }
        else
        {
            ySpeed -= stateMachine.CharacterData.Gravity;
        }

        Vector3 veritcal = new Vector3( 0, ySpeed, 0 );

        speed = Mathf.Lerp( speed, averageMoveSpeed, 10 * Time.deltaTime );

        stateMachine.MoveComponent.Move( ( direction * speed + veritcal ) );
    }
}
