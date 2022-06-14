using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    Transform cameraTr;
    [SerializeField]
    float gravity;
    [SerializeField]
    float jumpForce;

    CharacterController characterController;
    Animator animator;
    AnimatorStateInfo combatAnimInfo;

    bool isMoving;
    bool isCombat;

    float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        combatAnimInfo = animator.GetCurrentAnimatorStateInfo( 1 );
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool( "Click", false );


        /* if( animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo( 1 ).IsName("Hit1") )
         {
             animator.SetBool( "Hit1", false );
         }

         if ( animator.GetCurrentAnimatorStateInfo( 1 ).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Hit1" ) )
         {
             animator.SetBool( "Hit2", false );
         }

         if ( animator.GetCurrentAnimatorStateInfo( 1 ).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Hit1" ) )
         {
             animator.SetBool( "Hit3", false );
             numOfClicks = 0;
         }*/

        Move();
        Combat();
    }


    void Move()
    {
        float x = Input.GetAxis( "Horizontal" );
        float z = Input.GetAxis( "Vertical" );

        Vector3 direction = new Vector3( x, 0, z );

        float inputMagnitude = Mathf.Clamp01( direction.magnitude );

        if( Input.GetKey(KeyCode.LeftShift) )
        {
            inputMagnitude /= 2;
        }

        direction = Quaternion.AngleAxis( cameraTr.rotation.eulerAngles.y, Vector3.up ) * direction;
        direction.Normalize();

        isMoving = inputMagnitude > 0.001f;
        animator.SetFloat( "Input magnitude", inputMagnitude, 0.05f, Time.deltaTime );
        animator.SetBool( "IsMoving", isMoving );

        if ( direction != Vector3.zero )
        {
            Quaternion toRotation = Quaternion.LookRotation( direction, Vector3.up );
            transform.rotation = Quaternion.RotateTowards( transform.rotation, toRotation, rotationSpeed * Time.deltaTime );
        }

        if( characterController.isGrounded )
        {
            ySpeed = -gravity;
            if ( Input.GetKeyDown(KeyCode.Space))
            {
                ySpeed = jumpForce;
            }
            
        }
        else
        {
            ySpeed -= gravity * Time.deltaTime;
        }



        Vector3 veritcal = new Vector3( 0, ySpeed, 0 );

        characterController.Move( ( direction* inputMagnitude * speed + veritcal ) * Time.deltaTime );
    }


    float nextFireTime = 0.0f;
    float numOfClicks = 0;
    float cooldownTime = 2f;
    float lastClickedTime = 0.0f;
    void Combat()
    {
        //isCombat = animator.GetBool( "IsHit" );
        if ( Input.GetKeyDown( KeyCode.Mouse0) )
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

        animator.SetBool( "Click", true );
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

    void ChangeAnimationTest()
    {
        if( Input.GetKeyDown(KeyCode.A) )
        {
            animator.SetFloat("Weapon", 1 );
            animator.SetFloat( "TriggerNumber", 16 );
            animator.SetTrigger( "Trigger" );
        }

        if( Input.GetKeyDown(KeyCode.D))
        {

        }
    }

    private void OnApplicationFocus( bool focus )
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
