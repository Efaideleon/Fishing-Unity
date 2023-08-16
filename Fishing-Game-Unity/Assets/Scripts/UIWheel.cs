using System;
using UnityEngine;
using UIControlsInterfaces;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using System.Runtime.CompilerServices;

public class UIWheel : MonoBehaviour, IWheel
{
    [SerializeField] private EnhancedTouchManager _enhancedTouchManager;
    public RectTransform RectTransform => GetComponent<RectTransform>();
    public event Action<float> OnRotate;
    
    private Vector2 _centerOfWheel => RectTransform.position;
    private Vector2 _previousTouchPosition;
    private Vector2 _localPos;
    public void Rotate(float angle)
    {
        RectTransform.Rotate(0,0,angle);
        OnRotate?.Invoke(-angle);
    } 

    private void TouchEventHandler(EnhancedTouch.Finger finger, Action<EnhancedTouch.Finger> action )
    {
        if (IsTouchInWheel(finger.screenPosition))
        {
            action(finger);
        } 
    }

    private void RotateWheelImage(EnhancedTouch.Finger finger)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, finger.screenPosition, null, out _localPos);
        Vector2 currentTouchPosition = finger.screenPosition; 
        Debug.Log($"center of wheel: {_centerOfWheel} + currentTouchPosition: {currentTouchPosition}");
        Vector2 fromVector = _previousTouchPosition - _centerOfWheel;
        Vector2 toVector = currentTouchPosition - _centerOfWheel;
        float angle = Vector2.SignedAngle(fromVector, toVector);
        Debug.Log($"Angle: {angle}");
        Rotate(angle);
        _previousTouchPosition = currentTouchPosition;
    }

    private void OnEnable()
    {
        _enhancedTouchManager.OnStartTouch += finger => TouchEventHandler(finger, (finger) => _previousTouchPosition = finger.screenPosition ); 
        _enhancedTouchManager.OnMoveTouch += finger => TouchEventHandler(finger, RotateWheelImage);
        _enhancedTouchManager.OnEndTouch += finger => TouchEventHandler(finger, (finger) => Rotate(0));
    } 

    private void OnDisable()
    {
        _enhancedTouchManager.OnStartTouch += finger => TouchEventHandler(finger, (finger) => _previousTouchPosition = finger.screenPosition ); 
        _enhancedTouchManager.OnMoveTouch += finger => TouchEventHandler(finger, RotateWheelImage);
        _enhancedTouchManager.OnEndTouch += finger => TouchEventHandler(finger, (finger) => Rotate(0));
    }

    private bool IsTouchInWheel(Vector2 touchPosition) => RectTransformUtility.RectangleContainsScreenPoint(RectTransform, touchPosition);
}




