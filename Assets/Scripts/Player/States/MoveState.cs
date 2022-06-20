
using UnityEngine;

public class MoveState : State
{
    [SerializeField]
    float MoveSpeed;
    [SerializeField]
    float JumpForce;


    CharacterStateMachine stateMachine;
    float ySpeed;
    public override void OnStateEnter( StateMachine stateMachine )
    {
        this.stateMachine = stateMachine as CharacterStateMachine;
        ySpeed = -9.8f;
    }

    public override void OnStateExit( StateMachine stateMachine )
    {

    }

    public override void UpdateState( )
    {
        Move();
        //Combat();
    }

    void Move()
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


        if ( stateMachine.MoveComponent.IsGrounded() )
        {
            stateMachine.Animator.SetBool( "Jump", false );
            ySpeed = -stateMachine.CharacterData.Gravity;
            if ( Input.GetKeyDown( KeyCode.Space ) )
            {
                stateMachine.Animator.SetBool( "Jump", true );
                ySpeed = JumpForce;
            }
        }
        else
        {
            ySpeed -= stateMachine.CharacterData.Gravity * Time.deltaTime;
        }





        Vector3 veritcal = new Vector3( 0, ySpeed, 0 );

        stateMachine.MoveComponent.Move( ( direction * inputMagnitude * MoveSpeed + veritcal ) );
    }

    void Combat()
    {
        //isCombat = animator.GetBool( "IsHit" );
        if ( Input.GetKeyDown( KeyCode.Mouse0 ) )
        {
            OnClick();
            //OnClick();
            //animator
            //animator.Set
            //animator.SetBool( "IsHit", true );
        }
    }

    void OnClick()
    {
        //lastClickedTime = Time.time;
        //numOfClicks++;
        //numOfClicks = Mathf.Clamp( numOfClicks, 0, 3 );

        stateMachine.Animator.SetBool( "Click", true );
        //animator.SetTrigger( "TriggerTest" );
        //if ( numOfClicks == 1)
        //{
        //animator.SetBool( "IsHit", true );
        //}



        /*if ( numOfClicks >= 2 && animInfo.normalizedTime > 0.5f && animInfo.IsName( "Hit1" ) )
        {
            animator.SetBool( "Hit1", false );
            animator.SetBool( "Hit2", true );
        }
        if( numOfClicks >= 2 && animInfo.normalizedTime > 0.5f && animInfo.IsName( "Hit2" ) )
        {
            animator.SetBool( "Hit2", false );
            animator.SetBool( "Hit3", true );
        }*/

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
