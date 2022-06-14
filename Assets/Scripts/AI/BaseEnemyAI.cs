using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour, IHitable
{
    protected NavMeshAgent agent;
    protected Animator anim;
    protected Transform player;
    protected ProgressBar hpBar;

    [SerializeField]
    protected LayerMask whatIsGround, whatIsPlayer;

    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float runSpeed;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected float minDistanceToChase = 1.0f;
    //Patroling
    [SerializeField]
    protected float walkPointRange;
    //Atacking
    [SerializeField]
    protected float timeBetweenAttacks;
    //States
    [SerializeField]
    protected float sightRange;
    [SerializeField]
    protected float attackRange;

    protected float health;

    protected Vector3 walkPoint;
    protected bool walkPointSet;
    protected bool alreadyAttacked;

    protected bool playerInSightRange, playerInAttackRange;
    protected Vector3 offset = Vector3.zero;

    protected virtual void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hpBar = GetComponentInChildren<ProgressBar>();
        health = maxHealth;
    }

    protected virtual void Update()
    {
        //Check for sight and attack range
        playerInSightRange = IsPlayerInSight();
        playerInAttackRange = IsPlayerInSight();

        if ( !playerInSightRange && !playerInAttackRange ) Patroling();
        if ( playerInSightRange && !playerInAttackRange ) ChasePlayer();
        if ( playerInAttackRange && playerInSightRange ) AttackPlayer();

        OffsetMovement();
    }


    protected bool IsPlayerInSight(){ return Physics.CheckSphere( transform.position, sightRange, whatIsPlayer ); }
    protected bool IsPlayerInAttackRange() { return Physics.CheckSphere( transform.position, attackRange, whatIsPlayer ); }

    protected virtual void OffsetMovement()
    {
        offset = Vector3.Lerp( offset, Vector3.zero, Time.deltaTime * 10 );
        agent.Move( offset * Time.deltaTime );
    }

    protected virtual void Patroling()
    {
        agent.speed = walkSpeed;
        if ( !walkPointSet ) SearchWalkPoint();

        if ( walkPointSet )
            agent.SetDestination( walkPoint );

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if ( distanceToWalkPoint.magnitude < 1f )
        {
            walkPointSet = false;
            SetAnimState( EnemyAnimStates.IDLE );
        }
        else
        {
            SetAnimState( EnemyAnimStates.PATROL );
        }
    }
    protected void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range( -walkPointRange, walkPointRange );
        float randomX = Random.Range( -walkPointRange, walkPointRange );

        walkPoint = new Vector3( transform.position.x + randomX, transform.position.y, transform.position.z + randomZ );

        if ( Physics.Raycast( walkPoint, -transform.up, 2f, whatIsGround ) )
            walkPointSet = true;
    }

    protected void ChasePlayer()
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

    protected void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination( transform.position );
        transform.LookAt( player, Vector3.up );
        if ( !alreadyAttacked )
        {
            ///Attack code here
           /* Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code
           */
            alreadyAttacked = true;
            SetAnimState( EnemyAnimStates.ATTACK );
            Invoke( nameof( ResetAttack ), timeBetweenAttacks );
        }
        else
        {
            SetAnimState( EnemyAnimStates.IDLE );
        }
    }

    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    protected void DestroyEnemy()
    {
        Destroy( gameObject );
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position, attackRange );
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere( transform.position, sightRange );
    }


    public virtual void TakeHit( Vector3 direction, float damage )
    {
        offset = direction * 10;
        health -= damage;

        float progress = health / maxHealth;

        hpBar.SetProgress( progress );
        if ( health <= 0 ) Invoke( nameof( DestroyEnemy ), 0.5f );
    }



    protected void SetAnimState( EnemyAnimStates animState )
    {
        switch ( animState )
        {
            case EnemyAnimStates.IDLE:
                {
                    anim.SetBool( "Idle", true );
                    anim.SetBool( "Run", false );
                    anim.SetBool( "Patrol", false );
                    //anim.SetBool( "Attack", false );
                    break;
                }
            case EnemyAnimStates.PATROL:
                {
                    anim.SetBool( "Idle", false );
                    anim.SetBool( "Attack", false );
                    anim.SetBool( "Run", false );
                    anim.SetBool( "Patrol", true );
                    break;
                }
            case EnemyAnimStates.CHASE_PLAYER:
                {
                    anim.SetBool( "Patrol", false );
                    anim.SetBool( "Idle", false );
                    anim.SetBool( "Attack", false );
                    anim.SetBool( "Run", true );
                    break;
                }
            case EnemyAnimStates.ATTACK:
                {
                    anim.SetBool( "Attack", true );
                    break;
                }
        }
    }
}