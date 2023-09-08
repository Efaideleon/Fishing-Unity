using System;
using UIControlsInterfaces;
using UnityEngine;

public class UIBackButton : UIElementBase, IAccelerateButton
{
    public event Action<float> Backing;
    public void Accelerate(float speed)
    {
        Backing?.Invoke(speed);
    }
}
