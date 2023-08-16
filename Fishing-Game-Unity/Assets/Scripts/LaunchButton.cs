using UIControlsInterfaces;
using UnityEngine;
public class LaunchButton : MonoBehaviour, ILaunchButton 
{
    [SerializeField] private EnhancedTouchManager _enhancedTouchManager;
    public RectTransform RectTransform => GetComponent<RectTransform>();
    public void Launch()
    {
        Debug.Log("Launch");
    } 

    private void OnStartTouchHandler(Vector2 fingerPosition)
    {
        if (IsTouchingLaunchButton(fingerPosition))
            Launch();
    }

    public void OnEnable()
    {
        _enhancedTouchManager.OnStartTouch += context => OnStartTouchHandler(context.screenPosition);
    }

    public void OnDisable()
    {
        _enhancedTouchManager.OnStartTouch -= context => OnStartTouchHandler(context.screenPosition);
    }

    private bool IsTouchingLaunchButton(Vector2 fingerPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(RectTransform, fingerPosition);
    }
}
