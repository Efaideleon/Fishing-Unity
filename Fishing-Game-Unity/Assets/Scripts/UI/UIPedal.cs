using System;
using UIControlsInterfaces;

public class UIPedal : UIElementBase, IAccelerateButton 
{
    public event Action<float> Pressing;
    public void Accelerate(float speed) => Pressing?.Invoke(speed);
}
