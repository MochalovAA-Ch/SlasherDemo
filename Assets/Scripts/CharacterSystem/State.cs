using UnityEngine;

public abstract class State:MonoBehaviour
{
    public bool ShouldExit;  //Флаг выхода из состояния
    public abstract void UpdateState();
    public abstract void OnStateExit( StateMachine stateMachine );
    public abstract void OnStateEnter( StateMachine stateMachine );
}
