using System;
using UIControlsInterfaces;
using UnityEngine;

public class UIPedal : UIElementBase, IAccelerateButton 
{
    public event Action<Vector2> OnPress;
    public void Accelerate(Vector2 direction) => OnPress?.Invoke(direction);
}
