using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControlsInterfaces
{
    public interface IWheel
    {
        void Rotate(float angle);
    }   

    public interface IPedal
    {
        void Accelerate(Vector2 direction);
    }

    public interface ILaunchButton
    {
        void Launch();
    }
}