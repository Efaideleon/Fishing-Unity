using System;
using UnityEngine;

namespace UIUtilities
{
    public class UIElementRotator<T> where T : MonoBehaviour 
    {
        private readonly T _uiElement;
        private Vector2 _centerOfElement;
        private Vector2 _previousTouchPosition {get; set;}

        public UIElementRotator(T uiElement)
        {
            if (uiElement == null)
            {
                throw new ArgumentNullException(nameof(uiElement), "UIElementRotator cannot be initialized with null");
            }
            _uiElement = uiElement;
            _centerOfElement = _uiElement.GetComponent<RectTransform>().position;
        }

        public void SetPreviousTouchPosition(Vector2 fingerPosition)
        {
            _previousTouchPosition = fingerPosition;
        }

        public float CalculateRelativeRotationAngle(Vector2 fingerPosition)
        {
            if (_uiElement == null)
            {
                throw new ArgumentNullException(nameof(_uiElement), "UIElementRotator cannot be initialized with null");
            }
            RectTransform rectTransform = _uiElement.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_uiElement.GetComponent<RectTransform>(), fingerPosition, null, out _);
            Vector2 fromVector = _previousTouchPosition - _centerOfElement;
            Vector2 toVector = fingerPosition - _centerOfElement;
            float angle = Vector2.SignedAngle(fromVector, toVector);
            _previousTouchPosition = fingerPosition;
            return angle;
        }
    }
}
