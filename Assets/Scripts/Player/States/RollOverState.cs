using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollOverState : State
{
    float ySpeed;
    Vector3 direction;

    Timer timer = new Timer( 1f );
    public override void OnStateEnter( StateMachine stateMachine )
    {
        ySpeed = -9.8f;
        float x = Input.GetAxis( "Horizontal" );
        float z = Input.GetAxis( "Vertical" );
        direction = new Vector3( x, 0, z );

        direction = Quaternion.AngleAxis( stateMachine.GetCameraRotationEulers().y, Vector3.up ) * direction;
        direction.Normalize();

        timer.ResetTimer();

    }

    public override void OnStateExit( StateMachine stateMachine )
    {

    }

    public override void UpdateState( StateMachine stateMachine )
    {
        if( timer.CheckTimer( Time.deltaTime ) )
        {
            return;
        }

        if ( stateMachine.CharacterController.isGrounded )
        {
            ySpeed = -stateMachine.CharacterData.Gravity;
        }
        else
        {
            ySpeed -= stateMachine.CharacterData.Gravity;
        }



        Vector3 veritcal = new Vector3( 0, ySpeed, 0 );

        stateMachine.CharacterController.Move( ( direction * stateMachine.CharacterData.HorizontalMoveSpeed + veritcal ) * Time.deltaTime );
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
