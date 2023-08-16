using System.Collections;
using System.Collections.Generic;
using UIControlsInterfaces;
using UnityEngine;

public class UIElementBase : MonoBehaviour, IUIElement
{
    public RectTransform RectTransform { get; private set; }

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public bool IsTouchingElement(Vector2 position) => 
        RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), position);
}
