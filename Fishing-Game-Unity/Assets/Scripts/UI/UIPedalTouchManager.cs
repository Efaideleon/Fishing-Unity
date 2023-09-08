using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class UIPedalTouchManager :  UITouchManagerBase
{
    private UIPedal _uiPedal;

    private void Start()
    {
        _uiPedal = GetComponent<UIPedal>();
        Debug.Log(_uiPedal);
    }
    protected override void OnStartTouchHandler(Finger finger)
    {
        if (_uiPedal.IsTouchingElement(finger.screenPosition))
        {
            _uiPedal.Accelerate(1);
        }
    }

    protected override void OnEndTouchHandler(Finger finger)
    {
        if (_uiPedal.IsTouchingElement(finger.currentTouch.startScreenPosition))
        {
            _uiPedal.Accelerate(0);
        }
    }
}
