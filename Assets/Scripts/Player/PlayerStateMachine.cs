using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : CharacterStateMachine
{

    CharacterController characterController;

    bool RollOverBtnPressed;

    private void Start()
    {
        //Init();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        statesList = new List<State>()
        {
            new MoveState(), new RollOverState()
        };

        moveComponent = new PlayerMoveComponent(characterController);

        currentState = statesList.Find(x => x is MoveState);
        currentState.OnStateEnter(this);
    }


    void Update()
    {
        ChangeStateLogic();
        currentState.UpdateState();
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

    private void OnAnimatorMove()
    {
        Vector3 velocity = anim.deltaPosition;
        velocity.y = moveComponent.VerticalMovement * Time.deltaTime;

        characterController.Move( velocity );
        //moveComponent.MoveY()
    }
}
