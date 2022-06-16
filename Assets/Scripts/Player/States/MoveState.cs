
using UnityEngine;

public class MoveState : State
{
    float ySpeed;
    public override void OnStateEnter( StateMachine stateMachine )
    {
        ySpeed = -9.8f;
    }

    public override void OnStateExit( StateMachine stateMachine )
    {

    }

    public override void UpdateState( StateMachine stateMachine )
    {
        
        float x = Input.GetAxis( "Horizontal" );
        float z = Input.GetAxis( "Vertical" );

        Vector3 direction = new Vector3( x, 0, z );

        float inputMagnitude = Mathf.Clamp01( direction.magnitude );

        if ( Input.GetKey( KeyCode.LeftShift ) )
        {
            inputMagnitude /= 2;
        }

        direction = Quaternion.AngleAxis( stateMachine.GetCameraRotationEulers().y, Vector3.up ) * direction;
        direction.Normalize();

        bool isMoving = inputMagnitude > 0.001f;

        stateMachine.Animator.SetFloat( "Input magnitude", inputMagnitude, 0.05f, Time.deltaTime );
        stateMachine.Animator.SetBool( "IsMoving", isMoving );

        if ( direction != Vector3.zero )
        {
            Quaternion toRotation = Quaternion.LookRotation( direction, Vector3.up );
            stateMachine.Transform.rotation = Quaternion.RotateTowards( stateMachine.Transform.rotation, toRotation, stateMachine.CharacterData.RotationSpeed * Time.deltaTime );
        }

        if ( stateMachine.CharacterController.isGrounded )
        {
            ySpeed = -stateMachine.CharacterData.Gravity;
            if ( Input.GetKeyDown( KeyCode.Space ) )
            {
                ySpeed = stateMachine.CharacterData.JumpForce;
            }
        }
        else
        {
            ySpeed -= stateMachine.CharacterData.Gravity;
        }



        Vector3 veritcal = new Vector3( 0, ySpeed, 0 );

        stateMachine.CharacterController.Move( ( direction * inputMagnitude * stateMachine.CharacterData.HorizontalMoveSpeed + veritcal ) * Time.deltaTime );
    }
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
