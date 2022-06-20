using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveComponent : MoveComponent
{
    NavMeshAgent navMeshAgent;
    float defaultHeight;
    private AIMoveComponent() { }
    public AIMoveComponent( NavMeshAgent navMeshAgent )
    {
        this.navMeshAgent = navMeshAgent;
        //defaultHeight = characterController.height;
    }


    public override void Move( Vector3 direction )
    {
        navMeshAgent.Move( direction );
    }

    public override void SetDestination( Vector3 destination )
    {
        navMeshAgent.SetDestination( destination );
    }
    public override bool IsGrounded()
    {
        throw new System.NotImplementedException();
    }

    public override void SetDefaultHeight()
    {
        throw new System.NotImplementedException();
    }

    public override void SetSpeed( float speed )
    {
        navMeshAgent.speed = speed;
    }

    public override void MultiplyHeght( float multiplier )
    {
       throw new System.NotImplementedException();
    }
}
