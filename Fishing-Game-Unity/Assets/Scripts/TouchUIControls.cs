using UnityEngine;
using EnhancedTounch = UnityEngine.InputSystem.EnhancedTouch;
public class TouchUIControls : MonoBehaviour
{
    [SerializeField] private EnhancedTouchManager enhancedTouchManager;
    [SerializeField] private UIWheel uiWheel;
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
            Debug.Log("Touch Started on Wheel");
            previousTouchPosition = finger.screenPosition;
            Debug.Log("Center of Wheel: " + centerOfWheel);
        }
        else
        {
            Debug.Log("Touch Started on Screen");
        }
    }

    public void OnMoveTouch(EnhancedTounch.Finger finger, float time)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(uiWheel.GetRectTransform(), finger.screenPosition))
        {
            Vector2 currentTouchPosition = finger.screenPosition; 
            Debug.Log("Touch Moved on Wheel");

            Debug.Log("Previous Touch" + previousTouchPosition);
            Vector2 fromVector = previousTouchPosition - centerOfWheel;
            Vector2 toVector = currentTouchPosition - centerOfWheel;

            float angle = Vector2.SignedAngle(fromVector, toVector);
            Debug.Log("Angle: " + angle);
            uiWheel.Rotate(angle);
            previousTouchPosition = currentTouchPosition;
            Debug.Log("Current Touch" + currentTouchPosition);
        }
        else
        {
            Debug.Log("Touch Moved on Screen");
        }   
    }

    public void OnEndTouch(EnhancedTounch.Finger finger, float time)
    {
        Debug.Log("Touch Ended");
    }
}
