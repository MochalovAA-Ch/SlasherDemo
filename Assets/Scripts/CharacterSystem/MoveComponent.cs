using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveComponent
{
    protected float verticalMovement;

    public float VerticalMovement => verticalMovement;
    public abstract void MoveY(float val);
    public abstract void SetDestination(Vector3 destination);
    public abstract bool IsGrounded();

    public abstract void SetDefaultHeight();

    public abstract void MultiplyHeght( float multiplier );

}
