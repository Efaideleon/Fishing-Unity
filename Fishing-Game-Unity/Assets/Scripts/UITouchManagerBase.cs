using UIControlsInterfaces;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
public abstract class UITouchManagerBase : MonoBehaviour 
{
    [SerializeField] private EnhancedTouchManager _enhancedTouchManager;

    protected abstract void OnStartTouchHandler(EnhancedTouch.Finger finger);

    protected abstract void OnMoveTouchHandler(EnhancedTouch.Finger finger);

    protected abstract void OnEndTouchHandler(EnhancedTouch.Finger finger);

    public void OnEnable()
    {
        _enhancedTouchManager.OnStartTouch += OnStartTouchHandler;
        _enhancedTouchManager.OnMoveTouch += OnMoveTouchHandler;
        _enhancedTouchManager.OnEndTouch += OnEndTouchHandler;
    }

    public void OnDisable()
    {
        _enhancedTouchManager.OnStartTouch -= OnStartTouchHandler;
        _enhancedTouchManager.OnMoveTouch -= OnMoveTouchHandler;
        _enhancedTouchManager.OnEndTouch -= OnEndTouchHandler;
    }
}
