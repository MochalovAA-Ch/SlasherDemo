using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveComponent : MoveComponent
{

    CharacterController characterController;
    float defaultHeight;
    private PlayerMoveComponent() { }
    public PlayerMoveComponent( CharacterController characterController )
    {
        this.characterController = characterController;
        defaultHeight = characterController.height;
    }


    public override void Move( Vector3 direction )
    {
        moveVector = direction;
        //characterController.Move(direction);
    }
    public override void SetDestination( Vector3 destination )
    {
        throw new System.NotImplementedException();
    }
    public override bool IsGrounded()
    {
        return characterController.isGrounded;
    }

    public override void SetDefaultHeight()
    {
        characterController.height = defaultHeight;
    }

    public override void SetSpeed(float speed)
    {
        throw new System.NotImplementedException();
    }  

    public override void MultiplyHeght(float multiplier)
    {
        characterController.height = defaultHeight * multiplier;
        //throw new System.NotImplementedException();
    }
}
