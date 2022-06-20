using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : CharacterStateMachine
{
    CharacterController characterController;
    bool RollOverBtnPressed;

    public ProgressBar progressBar;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Init( new PlayerMoveComponent( characterController ) );
        currentState = statesList.Find(x => x is MoveState);
        currentState.OnStateEnter(this);
    }


    List<SimpleDamage> damageSlots = new List<SimpleDamage>(10);

    void Update()
    {
        OnClick();

        currentState.UpdateState();
        characterController.Move( moveComponent.MoveVector * Time.deltaTime );
        ChangeStateLogic();
        ProcessDamage();
        //ResetAnimParameters();
        /*if (currentState.ShouldExit)
        {
            ChangeStateLogic();
        }
        else
        {
            currentState.UpdateState();
        }*/

        /*if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeState(new RollOverState());
        }*/
    }

    private void LateUpdate()
    {
        ResetAnimParameters();
    }

    private void OnClick()
    {
        if( Input.GetKeyDown( KeyCode.Mouse0 ) )
            anim.SetBool( "Click", true );
    }

    void ResetAnimParameters()
    {
        anim.SetBool( "Click", false );
    }

    public override void ChangeStateLogic()
    {
        RollOverBtnPressed = Input.GetKeyDown(KeyCode.V);
        if( currentState.ShouldExit )
        {
            if (currentState is RollOverState)
            {
                ChangeState(statesList.Find(x => x is MoveState));
            }
        }
        else
        {
            if( currentState is MoveState )
            {
                if (RollOverBtnPressed)
                    ChangeState(statesList.Find(x => x is RollOverState));
            }
        }
    }

    public override void TakeDamage( float damage )
    {
        hp -= damage;
        progressBar.SetProgress( hp / characterData.Health );

        Debug.Log( "Taken damage " + damage );
        Debug.Log( "Remains health" + hp );
    }

    void ProcessDamage()
    {
        for ( int i = 0; i < damageSlots.Count; i++ )
        {
            if ( damageSlots[i] == null )
                continue;

            damageSlots[i].UpdateDamage();
            if ( damageSlots[i].IsEnded && !damageSlots[i].IsInTrigger )
                damageSlots[i] = null;
        }
    }

    void AddDamageToSlots( SimpleDamage damage)
    {
        if ( damageSlots.Count < 10 )
        {
            if( !damageSlots.Contains( damage ) )
                damageSlots.Add( damage );

        }
        else
        {
            if ( !damageSlots.Contains( damage ) )
            {
                for ( int i = 0; i < damageSlots.Count; i++ )
                {
                    if ( damageSlots[i] == null ) damageSlots[i] = damage;
                }
            }
        }

        damage.ApplyDamage( this );
    }

    bool CheckForEnterDamage( Collider other)
    {
        SimpleDamage damage = other.GetComponent<SimpleDamage>();
        if ( damage != null )
        {
            AddDamageToSlots( damage );
            return true;
        }
        return false;    
    }

    void CheckForStayDamage( Collider other )
    {
        SimpleDamage damage = other.GetComponent<SimpleDamage>();
        if ( damage != null )
        {
            SimpleDamage currentDamage = damageSlots.Find( x => x == damage );
            if( currentDamage != null )
            {
                currentDamage.IsInTrigger = true;
            }
        }
    }

    void CheckForExitDamage( Collider other )
    {
        SimpleDamage damage = other.GetComponent<SimpleDamage>();
        if ( damage != null ) damage.IsInTrigger = false;
    }
        private void OnTriggerEnter( Collider other )
    {
        CheckForEnterDamage( other );
    }

    private void OnTriggerStay( Collider other )
    {
        CheckForStayDamage( other );
        //CheckForDamage( other );
    }

    private void OnTriggerExit( Collider other )
    {
        CheckForExitDamage( other );
    }
}
