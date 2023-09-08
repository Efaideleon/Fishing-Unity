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
        void Accelerate(float speed);
    }

    public interface ILaunchButton
    {
        void Launch();
    }
}