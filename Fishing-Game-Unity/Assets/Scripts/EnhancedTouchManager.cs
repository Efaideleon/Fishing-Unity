using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class EnhancedTouchManager : MonoBehaviour
{
    public delegate void StartTouchEvent(EnhancedTouch.Finger finger);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(EnhancedTouch.Finger finger);
    public event EndTouchEvent OnEndTouch;
    public delegate void MoveTouchEvent(EnhancedTouch.Finger finger);
    public event MoveTouchEvent OnMoveTouch;
    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
        EnhancedTouch.Touch.onFingerMove += FingerMove;
        EnhancedTouch.Touch.onFingerUp += FingerUp;
    }

    private void OnDisable()
    {
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouch.Touch.onFingerMove -= FingerMove;
        EnhancedTouch.Touch.onFingerUp -= FingerUp;
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        
        OnStartTouch?.Invoke(finger);
    }

    private void FingerUp(EnhancedTouch.Finger finger)
    {
        OnEndTouch?.Invoke(finger);
    }    

    private void FingerMove(EnhancedTouch.Finger finger)
    {
        OnMoveTouch?.Invoke(finger);
    }
}
