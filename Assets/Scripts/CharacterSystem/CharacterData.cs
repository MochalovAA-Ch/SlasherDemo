using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterData", menuName ="ScriptableObjects/ChracterData")]
public class CharacterData : ScriptableObject
{
    public int HorizontalMoveSpeed;
    public int VerticalMoveSpeed;
    public int Gravity;
    public int RotationSpeed;
    public int JumpForce;
}
