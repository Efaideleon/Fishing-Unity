using System;
using UnityEngine;
using UIControlsInterfaces;
public class UIWheel : UIElementBase, IWheel
{
    public event Action<float> Rotating;
    public void Rotate(float angle)
    {
        RectTransform.Rotate(0,0,angle);
        float clampAngle = Mathf.Clamp(angle, -1.8f, 1.8f);
        Rotating?.Invoke(-clampAngle);
    } 
}




