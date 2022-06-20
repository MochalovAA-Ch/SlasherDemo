using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapon : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField]
    float attackTime;

    Timer atackTimer;

    bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        atackTimer = new Timer( attackTime );
    }

    // Update is called once per frame
    void Update()
    {
        if( isAttack )
        {
            if ( atackTimer.CheckTimer( Time.deltaTime ) )
            {
                atackTimer.ResetTimer();
                isAttack = false;
            }
        }
    }


    public void Attack()
    {
        isAttack = true;
        atackTimer.ResetTimer();
    }

    private void OnTriggerEnter( Collider other )
    {
        PlayerStateMachine playerStateMachine = other.GetComponent<PlayerStateMachine>();
        if( playerStateMachine  != null && isAttack )
        {
            playerStateMachine.TakeDamage( damage );
            //isAttack = false;
            //Debug.Log( "Hitted player" );
        }
    }

}
