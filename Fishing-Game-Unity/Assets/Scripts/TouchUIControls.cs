using UnityEngine;
using UnityEngine.Rendering.Universal;
using EnhancedTounch = UnityEngine.InputSystem.EnhancedTouch;
public class TouchUIControls : MonoBehaviour
{
    [SerializeField] private EnhancedTouchManager enhancedTouchManager;
    [SerializeField] private UIWheel uiWheel;
    [SerializeField] private UIPedal uiPedal;
    [SerializeField] private LaunchButton launchButton;
    private Vector2 centerOfWheel;
    private Vector2 previousTouchPosition;
    [SerializeField] private Camera mainCamera;
    void Start()
    {
        centerOfWheel = uiWheel.GetRectTransform().anchoredPosition;
    } 
    
    private void OnEnable()
    {
        enhancedTouchManager.OnStartTouch += OnStartTouch;
        enhancedTouchManager.OnMoveTouch += OnMoveTouch;
        enhancedTouchManager.OnEndTouch += OnEndTouch;
    }

    private void OnDisable()
    {
        enhancedTouchManager.OnStartTouch -= OnStartTouch;
        enhancedTouchManager.OnMoveTouch -= OnMoveTouch;
        enhancedTouchManager.OnEndTouch -= OnEndTouch;
    }

    public void OnStartTouch(EnhancedTounch.Finger finger, float time)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(uiWheel.GetRectTransform(), finger.screenPosition))
        {
            previousTouchPosition = finger.screenPosition;
        }
        else if (IsTouchingLaunchButton(finger.screenPosition))
        {
            launchButton.OnPress();
        }
        else if (IsTouchingPedal(finger.screenPosition))
        {
            uiPedal.OnPress(new Vector2(0,1));
        }
    }

    public void OnMoveTouch(EnhancedTounch.Finger finger, float time)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(uiWheel.GetRectTransform(), finger.screenPosition))
        {
            Vector2 currentTouchPosition = finger.screenPosition; 

            Vector2 fromVector = previousTouchPosition - centerOfWheel;
            Vector2 toVector = currentTouchPosition - centerOfWheel;

            float angle = Vector2.SignedAngle(fromVector, toVector);
            uiWheel.Rotate(angle);
            previousTouchPosition = currentTouchPosition;
        }
    }

    public void OnEndTouch(EnhancedTounch.Finger finger, float time)
    {
        if (IsTouchingPedal(finger.currentTouch.startScreenPosition))
        {
            Debug.Log("OnEndTouch");
            uiPedal.OnPress(new Vector2(0,0));
        }
    }

    private bool IsTouchingPedal(Vector2 fingerPosition)
    {
        Debug.Log("IsTouchingPedal");
        return RectTransformUtility.RectangleContainsScreenPoint(uiPedal.GetRectTransform(), fingerPosition);
    }

    private bool IsTouchingLaunchButton(Vector2 fingerPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(launchButton.GetRectTransform(), fingerPosition);
    }


}
