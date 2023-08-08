using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchButton : MonoBehaviour
{
    [SerializeField] private Player player;

    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }

    public void OnPress()
    {
        player.UseFishingRod();
    }
}
