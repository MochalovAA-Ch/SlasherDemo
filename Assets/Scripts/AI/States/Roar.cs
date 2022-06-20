using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roar : State
{
    [SerializeField]
    Component  activableComp;

    IActivable activable;

    protected void OnValidate()
    {
        /*if ( !( activableComp is IActivable ) )
            activable = null;*/
    }
    protected  void Awake()
    {
        /*if ( activableComp != null )
            activable = activableComp as IActivable;*/
        activable = activableComp.GetComponent<IActivable>();

    }

    public override void UpdateState() 
    {
        if( !ShouldExit )
        {
            activable?.Activate();
            ShouldExit = true;
        }

    }
    public override void OnStateExit( StateMachine stateMachine ) { }
    public override void OnStateEnter( StateMachine stateMachine ) 
    {
        ShouldExit = false;

    }
}
