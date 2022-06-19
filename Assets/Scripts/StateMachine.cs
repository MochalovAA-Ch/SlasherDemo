
using UnityEngine;
public abstract class StateMachine : MonoBehaviour
{
    protected State previosState;
    protected State currentState;
    protected State nextState;

    /*[SerializeField]
    protected Transform cameraTr;
    [SerializeField]
    protected CharacterData characterData; 
    protected MoveComponent moveComponent;
    */
   // CharacterController characterController; //Только для игрока
    //public CharacterController CharacterController => characterController;
    //NavMeshAgent navMesh //Только для АИ

    /*Animator anim; //
    public Animator Animator => anim; 
    public Transform Transform => transform;
    public CharacterData CharacterData => characterData;
    public MoveComponent MoveComponent => moveComponent;*/

    private void Start()
    {
        //characterController = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
        //currentState = new MoveState();
    }


    void Update()
    {

    }

    /*public Vector3 GetCameraRotationEulers()
    {
        return cameraTr.rotation.eulerAngles;
    }*/

    public void ChangeState( State newState )
    {
        if ( newState == currentState ) return;

        currentState.OnStateExit(this);
        currentState = newState;
        currentState.OnStateEnter(this);
    }

    public abstract void ChangeStateLogic();
}
