using System;
using UIControlsInterfaces;
using UnityEngine;

public class UIBackButton : UIElementBase, IAccelerateButton
{
    public event Action<Vector2> OnBack;
    public void Accelerate(Vector2 direction)
    {
        OnBack?.Invoke(direction);
    }
}
