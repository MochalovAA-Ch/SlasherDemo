
using UnityEngine;
using UnityEngine.AI;

public enum EnemyAnimStates
{
    IDLE,
    PATROL,
    CHASE_PLAYER,
    ATTACK
}

public class EnemyAI : MonoBehaviour, IHitable
{
    NavMeshAgent agent;
    Animator anim;
    Transform player;

    ProgressBar hpBar;

    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField]
    float maxHealth;
    [SerializeField]
    float runSpeed;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float minDistanceToChase = 1.0f;
    [SerializeField]
    AIWeapon weapon;


    float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    Vector3 offset = Vector3.zero;

    Timer patrolTimer;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hpBar = GetComponentInChildren<ProgressBar>();
        weapon = GetComponentInChildren<AIWeapon>();
        health = maxHealth;
        patrolTimer = new Timer( 2.0f );
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        OffsetMovement();
    }

    void OffsetMovement()
    {
        offset = Vector3.Lerp( offset, Vector3.zero, Time.deltaTime * 10 );
        agent.Move( offset * Time.deltaTime );
    }

    private void Patroling()
    {
        agent.speed = walkSpeed;
        if (!walkPointSet)
        {
            if( patrolTimer.CheckTimer( Time.deltaTime ) )
            {
                SearchWalkPoint();
                patrolTimer.ResetTimer();
            }
                
        }
            

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            SetAnimState( EnemyAnimStates.IDLE );
        }
        else
        {
            SetAnimState( EnemyAnimStates.PATROL );
        }
            
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.speed = runSpeed;
        
        float distance = Vector3.Distance( transform.position, player.position );
        if ( distance > minDistanceToChase )
        {
            SetAnimState( EnemyAnimStates.CHASE_PLAYER );
            agent.SetDestination( player.position );
        }
        else
        {
            SetAnimState( EnemyAnimStates.IDLE );
            agent.SetDestination( transform.position );
        }
            

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player, Vector3.up);
        

        if (!alreadyAttacked)
        {
            ///Attack code here
           /* Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code
           */
            alreadyAttacked = true;
            SetAnimState( EnemyAnimStates.ATTACK );
            weapon.Attack();
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        else
        {
            SetAnimState( EnemyAnimStates.IDLE );
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    public void TakeHit( Vector3 direction, float damage )
    {
        offset = direction * 10;
        health -= damage;

        float progress = health / maxHealth;

        hpBar.SetProgress( progress );
        if ( health <= 0 ) Invoke( nameof( DestroyEnemy ), 0.5f );
    }



    void SetAnimState( EnemyAnimStates animState )
    {
        switch ( animState )
        {
            case  EnemyAnimStates.IDLE:
            {
                anim.SetBool( "Break", true );
                anim.SetBool( "Run", false );
                anim.SetBool( "Patrol", false );
                    //anim.SetBool( "Attack", false );
                break;
            }
            case EnemyAnimStates.PATROL:
            {
                anim.SetBool( "Break", false );
                anim.SetBool( "Attack", false );
                anim.SetBool( "Run", false );
                anim.SetBool( "Patrol", true );
                break;
            }
            case EnemyAnimStates.CHASE_PLAYER:
            {
                anim.SetBool( "Patrol", false );
                anim.SetBool( "Break", false );
                anim.SetBool( "Attack", false );
                anim.SetBool( "Run", true );
                break;
            }
            case EnemyAnimStates.ATTACK:
            {
                anim.SetBool( "Attack", true );
                //anim.SetBool( "Run", true );
                break;
            }
        }
    }
}
