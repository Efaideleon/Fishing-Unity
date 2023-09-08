using System.Net.NetworkInformation;
using UnityEngine;

namespace FindObjectsInScene
{
    /// <summary>
    /// This class is used to find objects in the scene
    /// </summary>
    public static class FindInScene 
    {
        public static PlayerReferenceManager PlayerReferenceManager => FindObjectComponent<PlayerReferenceManager>("PlayerReferenceManager");
        public static GameInput GameInput => FindObjectComponent<GameInput>("GameInput");
        public static UIWheel Wheel => FindObjectComponent<UIWheel>("Wheel");
        public static UIPedal Pedal => FindObjectComponent<UIPedal>("Pedal");
        public static UIBackButton BackButton => FindObjectComponent<UIBackButton>("BackButton");
        public static UILaunchButton LaunchButton => FindObjectComponent<UILaunchButton>("LaunchButton");
        public static WaterPlaneMovement WaterPlaneMovement => FindObjectComponent<WaterPlaneMovement>("Water");
        public static ThrowItem BasketBall => FindObjectComponent<ThrowItem>("BasketBall");
        public static GameManager GameManager => FindObjectComponent<GameManager>("GameManager");
        private static T FindObjectComponent<T> (string name) where T : Component
        {
            if (!GameObject.Find(name).TryGetComponent(out T component))
            {
                Debug.LogWarning($"{name} not found");
                return null;
            }
            return component;
        }
    }
}
