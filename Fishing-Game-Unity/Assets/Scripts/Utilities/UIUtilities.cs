using UnityEngine;

namespace UIUtilities
{
    public class UIElementRotator<T> where T : MonoBehaviour 
    {
        private T _uiElement;
        private Vector2 _centerOfElement;
        private Vector2 _previousTouchPosition {get; set;}

        public UIElementRotator(T uiElement)
        {
            _uiElement = uiElement;
            _centerOfElement = _uiElement.GetComponent<RectTransform>().position;
        }

        public void SetPreviousTouchPosition(Vector2 fingerPosition)
        {
            _previousTouchPosition = fingerPosition;
        }

        public float CalculateRelativeRotationAngle(Vector2 fingerPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_uiElement.GetComponent<RectTransform>(), fingerPosition, null, out _);
            Vector2 fromVector = _previousTouchPosition - _centerOfElement;
            Vector2 toVector = fingerPosition - _centerOfElement;
            float angle = Vector2.SignedAngle(fromVector, toVector);
            _previousTouchPosition = fingerPosition;
            return angle;
        }
    }
}
