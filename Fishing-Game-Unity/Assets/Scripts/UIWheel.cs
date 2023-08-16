using System;
using UnityEngine;
using UIControlsInterfaces;

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

    private void TouchEventHandler(Vector2 fingerPosition, Action<Vector2> action )
    {
        if (IsTouchInWheel(fingerPosition))
        {
            action(fingerPosition);
        } 
    }

    private void RotateWheelImage(Vector2 fingerPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, fingerPosition, null, out _localPos);
        Vector2 currentTouchPosition = fingerPosition; 
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
        _enhancedTouchManager.OnStartTouch += finger => TouchEventHandler(finger.screenPosition, (fingerPosition) => _previousTouchPosition = fingerPosition); 
        _enhancedTouchManager.OnMoveTouch += finger => TouchEventHandler(finger.screenPosition, RotateWheelImage);
        _enhancedTouchManager.OnEndTouch += finger => TouchEventHandler(finger.currentTouch.startScreenPosition, (finger) => Rotate(0));
    } 

    private void OnDisable()
    {
        _enhancedTouchManager.OnStartTouch -= finger => TouchEventHandler(finger.screenPosition, (fingerPosition) => _previousTouchPosition = fingerPosition); 
        _enhancedTouchManager.OnMoveTouch -= finger => TouchEventHandler(finger.screenPosition, RotateWheelImage);
        _enhancedTouchManager.OnEndTouch -= finger => TouchEventHandler(finger.currentTouch.startScreenPosition, (finger) => Rotate(0));
    }

    private bool IsTouchInWheel(Vector2 touchPosition) => RectTransformUtility.RectangleContainsScreenPoint(RectTransform, touchPosition);
}




