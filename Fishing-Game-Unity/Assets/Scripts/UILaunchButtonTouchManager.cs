using UIControlsInterfaces;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class UILaunchButtonTouchManager : UITouchManagerBase
{
    private UILaunchButton _launchButton;

    void Start()
    {
        _launchButton = GetComponent<UILaunchButton>();
    }

    protected override void OnStartTouchHandler(Finger finger)
    {
        if (_launchButton.IsTouchingElement(finger.screenPosition))
            _launchButton.Launch();
    }
    protected override void OnMoveTouchHandler(Finger finger) {}
    protected override void OnEndTouchHandler(Finger finger) {}
}
