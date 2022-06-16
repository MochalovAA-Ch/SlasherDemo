using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //protected CharacterData characterData;
    // Update is called once per frame
    public abstract void UpdateState( StateMachine stateMachine );

    public abstract void OnStateExit( StateMachine stateMachine );
    public abstract void OnStateEnter( StateMachine stateMachine );
}
