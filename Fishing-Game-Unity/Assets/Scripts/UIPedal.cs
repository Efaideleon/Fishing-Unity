using System;
using UIControlsInterfaces;
using UnityEngine;

public class UIPedal : MonoBehaviour, IPedal
{
    [SerializeField] private EnhancedTouchManager _enhancedTouchManager;
    public RectTransform RectTransform => GetComponent<RectTransform>(); 
    public event Action<Vector2> OnPress;
    public void Accelerate(Vector2 direction) => OnPress?.Invoke(direction);

    public void OnStartTouchHandler(Vector2 fingerPosition)
    {
        if (IsTouchingPedal(fingerPosition))
        {
            Accelerate(Vector2.up);
        }
    }

    public void OnEndTouchHandler(Vector2 fingerPosition)
    {
        if (IsTouchingPedal(fingerPosition))
        {
            Accelerate(Vector2.zero);
        }
    }

    public void OnEnable()
    {
        _enhancedTouchManager.OnStartTouch += context => OnStartTouchHandler(context.screenPosition);
        _enhancedTouchManager.OnEndTouch += context => OnEndTouchHandler(context.currentTouch.screenPosition);
    }

    public void OnDisable()
    {
        _enhancedTouchManager.OnStartTouch -= context => OnStartTouchHandler(context.screenPosition);
        _enhancedTouchManager.OnEndTouch -= context => OnEndTouchHandler(context.currentTouch.screenPosition);
    }

    private bool IsTouchingPedal(Vector2 fingerPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(RectTransform, fingerPosition);
    }

}
