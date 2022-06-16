
using UnityEngine;
public class StateMachine : MonoBehaviour
{
    protected State previosState;
    protected State currentState;
    protected State nextState;

    [SerializeField]
    Transform cameraTr;
    [SerializeField]
    CharacterData characterData;

    CharacterController characterController;
    Animator anim;
    public Animator Animator => anim;
    public CharacterController CharacterController => characterController;
    public Transform Transform => transform;
    public CharacterData CharacterData => characterData;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        currentState = new MoveState();

    }


    void Update()
    {
        currentState.UpdateState( this );

        if( Input.GetKeyDown(KeyCode.V) )
        {
            
        }


    }

    public Vector3 GetCameraRotationEulers()
    {
        return cameraTr.rotation.eulerAngles;
    }

    public void MoveChar( Vector3 moveDirection )
    {
        characterController.Move( moveDirection );
    }

    protected void ChangeState( State newState )
    {
        if ( newState == currentState ) return;

        currentState.OnStateExit(this);
        currentState = newState;
        currentState.OnStateEnter(this);
    }
    
}
