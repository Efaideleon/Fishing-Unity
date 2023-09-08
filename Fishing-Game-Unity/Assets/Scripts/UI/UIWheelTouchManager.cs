using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UIUtilities;
public class UIWheelTouchManager : UITouchManagerBase
{
    private UIElementRotator<UIWheel> _uiElementRotator;
    private UIWheel _uiWheel;
    private void Start()
    {
        _uiWheel = GetComponent<UIWheel>();
        _uiElementRotator = new UIElementRotator<UIWheel>(_uiWheel);
    }
    protected override void OnStartTouchHandler(Finger finger) 
    {
        if (_uiWheel.IsTouchingElement(finger.screenPosition))
            _uiElementRotator.SetPreviousTouchPosition(finger.screenPosition);
    }
    protected override void OnMoveTouchHandler(Finger finger)
    {
        if (_uiWheel.IsTouchingElement(finger.screenPosition))
            _uiWheel.Rotate(_uiElementRotator.CalculateRelativeRotationAngle(finger.screenPosition));
        else
        {
            _uiWheel.Rotate(0);
        }
    }
    protected override void OnEndTouchHandler(Finger finger)
    {
        if (_uiWheel.IsTouchingElement(finger.currentTouch.startScreenPosition))
            _uiWheel.Rotate(0);
    }
}
