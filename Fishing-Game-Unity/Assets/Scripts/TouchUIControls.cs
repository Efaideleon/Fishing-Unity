using UnityEngine;
using EnhancedTounch = UnityEngine.InputSystem.EnhancedTouch;
public class TouchUIControls : MonoBehaviour
{
    [SerializeField] private EnhancedTouchManager enhancedTouchManager;
    [SerializeField] private UIWheel uiWheel;
    [SerializeField] private UIPedal uiPedal;
    [SerializeField] private LaunchButton launchButton;
    private bool moving;
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
        else if (RectTransformUtility.RectangleContainsScreenPoint(uiPedal.GetRectTransform(), finger.screenPosition))
        {
            uiPedal.OnPress(new Vector2(0,1));
            moving = true;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(launchButton.GetRectTransform(), finger.screenPosition))
        {
            launchButton.OnPress();
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
        if (moving)
        {
            uiPedal.OnPress(new Vector2(0,0));
            moving = false;
        }
    }
}
