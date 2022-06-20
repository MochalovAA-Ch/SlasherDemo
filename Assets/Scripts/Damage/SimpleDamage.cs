using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDamage : MonoBehaviour
{
    CharacterStateMachine character;

    [SerializeField]
    float duration;
    float currentDuration;

    [SerializeField]
    [Tooltip("С какой периодичностью проходит урон от данного источника")]
    float tick;

    float currentTick;

    Timer timer;

    [SerializeField]
    float damage;

    [SerializeField]
    bool isOverTime;

    public bool IsEnded { get; private set; }

    bool isInTrigger;
    public bool IsInTrigger
    {
        get { return isInTrigger; }
        set
        {
            isInTrigger = value;
            if ( isInTrigger )
                currentDuration = 0.0f;
        }
    }
    public void UpdateDamage()
    {
        
        if ( isOverTime )
        {
            if ( currentDuration >= duration )
            {
                IsEnded = true;
                return;
            }
            else
            {
                ApplyTickDamage();
            }

            currentDuration += Time.deltaTime;
        }
        else
        {
            ApplyTickDamage();
            IsEnded = true;
        }
        currentTick += Time.deltaTime;
    }

    void ApplyTickDamage()
    {
        if ( currentTick >= tick )
        {
            currentTick = 0.0f;
            character.TakeDamage( damage );
        }
    }

    public void ApplyDamage( CharacterStateMachine character )
    {
        currentTick = tick;
        IsEnded = false;
        this.character = character;
    }

    public void AddTick()
    {
        currentTick += Time.deltaTime;
    }
}
