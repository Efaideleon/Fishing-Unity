using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void SetSpeed(float speed);
    void Move(Vector2 movementVector);
}
