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
    Vector2 localPos;
    [SerializeField] private Camera mainCamera;
    void Start()
    {
        //centerOfWheel = uiWheel.GetRectTransform().anchoredPosition;
        centerOfWheel = uiWheel.GetRectTransform().position;
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
        if (IsTouchingWheel(finger.screenPosition))
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
        if (IsTouchingWheel(finger.screenPosition))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(uiWheel.GetRectTransform(), finger.screenPosition, null, out localPos);
            Vector2 currentTouchPosition = finger.screenPosition; 
            
            Debug.Log("center of wheel: " + centerOfWheel + " currentTouchPosition: " + currentTouchPosition);
            Vector2 fromVector = previousTouchPosition - centerOfWheel;
            Vector2 toVector = currentTouchPosition - centerOfWheel;

            float angle = Vector2.SignedAngle(fromVector, toVector);

            //float maxAllowedRotation = 45.0f; // example value
            //angle = Mathf.Clamp(angle, -maxAllowedRotation, maxAllowedRotation);
            Debug.Log("angle: " + angle);
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
        return RectTransformUtility.RectangleContainsScreenPoint(uiPedal.GetRectTransform(), fingerPosition);
    }

    private bool IsTouchingLaunchButton(Vector2 fingerPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(launchButton.GetRectTransform(), fingerPosition);
    }

    private bool IsTouchingWheel(Vector2 fingerPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(uiWheel.GetRectTransform(), fingerPosition);
    }


}
