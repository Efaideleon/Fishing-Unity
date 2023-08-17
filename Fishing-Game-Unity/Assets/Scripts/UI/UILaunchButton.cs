using System;
using UIControlsInterfaces;
using UnityEngine;
public class UILaunchButton : UIElementBase, ILaunchButton 
{
    public delegate void LaunchDelegate();
    public event LaunchDelegate OnLaunch;
    public void Launch()
    {
        OnLaunch?.Invoke();
    } 
}
