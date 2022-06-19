using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Collider previousTarget;

    [SerializeField]
    LayerMask layer;

    // Update is called once per frame
    void Update()
    {
        CheckRayCast();
    }

    void CheckRayCast()
    {
        Ray ray = new Ray( transform.position, target.position - transform.position );
        RaycastHit raycastHit;
        Physics.Raycast( ray, out raycastHit );
        ProcessCollision( raycastHit.collider );
    }

    private void ProcessCollision( Collider currentTarget )
    {
        if ( currentTarget == null )
        {
            if ( previousTarget != null )
            {
                OnRayExit( previousTarget );
            }
        }
        else if ( previousTarget == currentTarget )
        {
            //Аналог OnStay event
        }
        else if ( previousTarget != null )
        {
            OnRayExit( previousTarget );
            OnRayEnter( currentTarget );
        }
        else
        {
            OnRayEnter( currentTarget );
        }

        // Remember this object for comparing with next frame.
        previousTarget = currentTarget;
    }

    void SetEnabledMeshRenderer( bool val, Collider collider)
    {
        MeshRenderer meshRenderer = collider.GetComponent<MeshRenderer>();
        if ( meshRenderer )
            meshRenderer.enabled = val;
    }

    void OnRayEnter( Collider collider )
    {
        SetEnabledMeshRenderer( false, collider );
        /*Enemy enemy = collider.GetComponent<Enemy>();
        if ( enemy != null )
            enemy.SetTargetMaterial();*/
    }

    void OnRayExit( Collider collider )
    {
        SetEnabledMeshRenderer( true, collider );
    }
}
