using UnityEngine;
using UnityEngine.AI;


public class Boss1StateMachine : CharacterStateMachine, IHitable
{
    NavMeshAgent agent;

    Transform playerTr;

    [SerializeField]
    float noticePlayerDistance;

    ProgressBar hpBar;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Init( new AIMoveComponent( agent ) );
        currentState = statesList.Find( x => x is Patrol );
        currentState.OnStateEnter( this );
        playerTr = GameObject.FindObjectOfType<PlayerStateMachine>().transform;

        hpBar = GetComponentInChildren<ProgressBar>();

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
        //characterController.Move( moveComponent.MoveVector * Time.deltaTime );
        ChangeStateLogic();
    }

    public override void TakeDamage( float damage )
    {

    }

    public override void ChangeStateLogic()
    {
        //Ќачало бо€, заметили игрока
        if ( currentState is Patrol )
        {
            
            if( noticePlayerDistance > Vector3.Distance( transform.position, playerTr.position ) )
            //Roar - крик, падение камней
                ChangeState( statesList.Find( x => x is Roar ) );
        }
        else 
        {
            if ( currentState.ShouldExit )
            {
                if ( currentState is Roar )
                {
                    ChangeState( statesList.Find( x => x is ChasePlayer ) );
                }
                else if ( currentState is ChasePlayer )
                {
                    ChangeState( statesList.Find( x => x is Attack ) );
                }
                else if ( currentState is Attack )
                {
                    ChangeState( statesList.Find( x => x is Break ) );
                }
                else if ( currentState is Break )
                {
                    ChangeState( statesList.Find( x => x is Roar ) );
                }

            }
        }
    }

    private void DestroyEnemy()
    {
        Destroy( gameObject );
    }

    public void TakeHit( Vector3 direction, float damage )
    {
        //offset = direction * 10;
        hp -= damage;

        float progress = hp / characterData.Health;

        hpBar.SetProgress( progress );
        if ( hp <= 0 ) Invoke( nameof( DestroyEnemy ), 0.5f );
    }
}
