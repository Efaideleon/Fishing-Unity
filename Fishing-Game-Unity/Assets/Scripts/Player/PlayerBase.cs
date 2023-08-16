using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class PlayerBase : NetworkBehaviour, IPlayer
{
    public abstract void Move(Vector2 direction);
    public abstract void Turn(float angle);

    private protected static float CalculateSpeed(Vector3 movementVector)
    {
        if (movementVector.x !=0)
            return 6000f;
        else if (movementVector.z != 0)
            return 200f;
        else if (movementVector.x != 0 && movementVector.z != 0)
            return 3000f;
        else 
            return 0f;
    } 
}
