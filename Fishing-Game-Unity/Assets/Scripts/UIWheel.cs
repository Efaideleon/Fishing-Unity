using System;
using UnityEngine;
using UIControlsInterfaces;
public class UIWheel : UIElementBase, IWheel
{
    public event Action<float> OnRotate;
    public void Rotate(float angle)
    {
        RectTransform.Rotate(0,0,angle);
        OnRotate?.Invoke(-angle);
    } 
}




