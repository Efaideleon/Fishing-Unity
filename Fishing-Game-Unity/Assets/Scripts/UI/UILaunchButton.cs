using System;
using UIControlsInterfaces;
using UnityEngine;
public class UILaunchButton : UIElementBase, ILaunchButton 
{
    public delegate void LaunchDelegate();
    public event LaunchDelegate Launched;
    public void Launch()
    {
        Launched?.Invoke();
    } 
}
