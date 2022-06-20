using UnityEngine;


//Стойка перерыва, передышки между определенными действиями
public class Break : State
{
    CharacterStateMachine stateMachine;

    [SerializeField]
    float breakTime;

    Timer breakTimer;
    public override void UpdateState()
    { 
        if( breakTimer.CheckTimer( Time.deltaTime ) )
        {
            ShouldExit = true;
        }
    }
    public override void OnStateExit( StateMachine stateMachine_ )
    {
        stateMachine.Animator.SetBool( "Break", false );
    }
    public override void OnStateEnter( StateMachine stateMachine_ )
    {
        breakTimer = new Timer( breakTime );
        stateMachine = stateMachine_ as CharacterStateMachine;
        stateMachine.Animator.SetBool( "Break", true );
        ShouldExit = false;
    }
}
