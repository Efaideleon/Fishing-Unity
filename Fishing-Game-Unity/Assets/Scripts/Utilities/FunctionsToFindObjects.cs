using UnityEngine;

namespace FindObjectsInScene
{
    public static class FindInScene 
    {
        public static PlayerReferenceManager PlayerReferenceManager => FindObjectComponent<PlayerReferenceManager>("PlayerReferenceManager");
        public static GameInput GameInput => FindObjectComponent<GameInput>("GameInput");
        public static UIWheel Wheel => FindObjectComponent<UIWheel>("Wheel");
        public static UIPedal Pedal => FindObjectComponent<UIPedal>("Pedal");

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
