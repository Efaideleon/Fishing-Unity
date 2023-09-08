using Unity.Netcode;
using UnityEngine;

public abstract class PlayerBase : NetworkBehaviour, IPlayer
{
    public abstract void SetSpeed(float speed);
    public abstract void SetAngle(float angle);

    private protected static float CalculateForce(float speed)
    {
        if (speed!=0)
            return speed * 10000f;
        return 0f;
    } 
}
