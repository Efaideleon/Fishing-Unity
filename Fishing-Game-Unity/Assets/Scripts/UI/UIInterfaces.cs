using UnityEngine;

namespace UIControlsInterfaces
{
    public interface IUIElement
    {
        RectTransform RectTransform { get; }
    }
    public interface IWheel
    {
        void Rotate(float angle);
    }   

    public interface IAccelerateButton
    {
        void Accelerate(Vector2 direction);
    }

    public interface ILaunchButton
    {
        void Launch();
    }
}