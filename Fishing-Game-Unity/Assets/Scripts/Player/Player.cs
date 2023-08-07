using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FishingRod fishingRod;
    [SerializeField] FishingRodFactory fishingRodFactory;
    void Start()
    {
       fishingRod = fishingRodFactory.CreateFishingRod(transform.position);
    }

    void Update()
    {
        fishingRod.Hold(transform.position);
    }

    public void UseFishingRod()
    {
        fishingRod.Use(transform);
    }
}
