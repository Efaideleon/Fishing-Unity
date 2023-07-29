using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FishingRod fishingRod;
    [SerializeField] FishingRodFactory fishingRodFactory;
    // Start is called before the first frame update
    void Start()
    {
       fishingRod = fishingRodFactory.CreateFishingRod(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        fishingRod.Hold(transform.position);
    }

    public void UseFishingRod()
    {
        fishingRod.Use(transform.position);
    }
}
