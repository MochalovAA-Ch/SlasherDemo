using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapon : MonoBehaviour
{
    [SerializeField]
    float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter( Collider other )
    {
        PlayerStateMachine playerStateMachine = other.GetComponent<PlayerStateMachine>();
        if( playerStateMachine  != null)
        {
            playerStateMachine.TakeDamage( damage );
            //Debug.Log( "Hitted player" );
        }
    }

}
