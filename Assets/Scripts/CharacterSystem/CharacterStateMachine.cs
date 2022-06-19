using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStateMachine : StateMachine
{
    [SerializeField]
    protected CharacterData characterData;
    [SerializeField]
    protected Transform cameraTr;

    [SerializeField]
    protected List<State> statesList;

    protected MoveComponent moveComponent;
    protected Animator anim;

    public Animator Animator => anim;
    public Transform Transform => transform;
    public CharacterData CharacterData => characterData;
    public MoveComponent MoveComponent => moveComponent;

    public Vector3 GetCameraRotationEulers()
    {
        return cameraTr.rotation.eulerAngles;
    }

    protected void Init( MoveComponent moveComponent )
    {
        anim = GetComponent<Animator>();
        this.moveComponent = moveComponent;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //void Chan
}
