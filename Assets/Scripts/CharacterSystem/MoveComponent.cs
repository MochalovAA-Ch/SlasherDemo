using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveComponent
{
    public abstract void Move(Vector3 direction);
    public abstract void SetDestination(Vector3 destination);
    public abstract bool IsGrounded();

    public abstract void SetDefaultHeight();

    public abstract void MultiplyHeght( float multiplier );

}
