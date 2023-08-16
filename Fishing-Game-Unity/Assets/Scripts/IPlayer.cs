using UnityEngine;

public interface IPlayer
{
    void Turn(float angle);
    void Move(Vector2 direction);
}
