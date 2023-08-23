using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class UIBackButtonTouchManager : UITouchManagerBase 
{
    private UIBackButton _backButton;

    void Start()
    {
        _backButton = GetComponent<UIBackButton>();
    }
    protected override void OnEndTouchHandler(Finger finger)
    {
        if (_backButton.IsTouchingElement(finger.currentTouch.startScreenPosition))
            _backButton.Accelerate(Vector2.zero);
    }

    protected override void OnStartTouchHandler(Finger finger)
    {
        if (_backButton.IsTouchingElement(finger.screenPosition))
            _backButton.Accelerate(Vector2.down);
    }
}
