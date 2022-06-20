using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void Update();

    public void ApplyDamage( CharacterStateMachine character );
}
