using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class WallRender
{
    public GameObject wall;
    public Timer timer;

    public WallRender( GameObject wall_ )
    {
        wall = wall_;
        timer = new Timer( 10 );
    }

}

public class CameraVision : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Collider previousTarget;

    [SerializeField]
    LayerMask layer;

    List<WallRender> toEnableList;

    // Update is called once per frame
    void Update()
    {
        CheckRayCast();
        CheckWallsToEnabled();
    }

    private void Start()
    {
        toEnableList = new List<WallRender>();
    }

    void CheckWallsToEnabled()
    {
        for( int i = 0; i < toEnableList.Count; i++ )
        {
            if( toEnableList[i] != null )
            {
                if ( toEnableList[i].timer.CheckTimer( Time.deltaTime ) )
                {
                    SetEnabledMeshRenderer( true, toEnableList[i].wall );
                    toEnableList[i] = null;
                }
                    
            }
        }
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

    void SetEnabledMeshRenderer( bool val, GameObject gameObject)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if ( meshRenderer )
            meshRenderer.enabled = val;
    }

    void OnRayEnter( Collider collider )
    {
        SetEnabledMeshRenderer( false, collider.gameObject );
        /*Enemy enemy = collider.GetComponent<Enemy>();
        if ( enemy != null )
            enemy.SetTargetMaterial();*/
    }

    void OnRayExit( Collider collider )
    {
        WallRender wall = toEnableList.Find( x => x.wall == collider.gameObject );
        if( wall != null )
        {
            wall.timer.ResetTimer();
        }
        else
        {
            bool isAdded = false;
            for ( int i = 0; i < toEnableList.Count; i++ )
            {
                if ( toEnableList[i] == null )
                {
                    toEnableList.Add( new WallRender( collider.gameObject  ) );
                }
            }

            if ( !isAdded )
                toEnableList.Add( new WallRender( collider.gameObject ) );
        }

        //SetEnabledMeshRenderer( true, collider );
    }
}
