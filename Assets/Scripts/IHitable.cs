using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable 
{
    public void TakeHit( Vector3 direction, float damage );
}
