using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveComponent
{
    protected Vector3 moveVector;
    public Vector3 MoveVector => moveVector;

    public abstract void Move(Vector3 direction);

    public abstract void SetSpeed( float speed );
    public abstract void SetDestination(Vector3 destination);
    public abstract bool IsGrounded();

    public abstract void SetDefaultHeight();
    public abstract void MultiplyHeght( float multiplier );

}
