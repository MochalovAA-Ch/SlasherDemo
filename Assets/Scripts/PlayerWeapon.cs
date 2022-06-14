using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    Transform playerTr;
    Animator animator ;

    [SerializeField]
    float baseDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = FindObjectOfType<Player>().transform;
        animator = playerTr.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( !Global.isSwordInHitState ) return;

        IHitable hitable = other.GetComponent<IHitable>();

        if( hitable != null )
        {
            float multiplier = 1;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo( 1 );

            if ( stateInfo.IsName( "Hit2" ) ) multiplier = 2;
            else if ( stateInfo.IsName( "Hit3" ) ) multiplier = 3;
            hitable.TakeHit( playerTr.forward, baseDamage * multiplier );
        }
    }
}
