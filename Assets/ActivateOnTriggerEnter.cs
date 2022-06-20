using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTriggerEnter : MonoBehaviour
{
    public GameObject target;
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
        PlayerStateMachine player = other.GetComponent<PlayerStateMachine>();
        if( player != null )
        {
            target.SetActive( true );
            gameObject.SetActive( false );
        }

    }
}
