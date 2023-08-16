using UIControlsInterfaces;
using UnityEngine;
public class UILaunchButton : UIElementBase, ILaunchButton 
{
    public void Launch()
    {
        Debug.Log("Launch");
    } 
}
