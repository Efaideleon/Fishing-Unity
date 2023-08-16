using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FishingRodFactory : MonoBehaviour
{
    [SerializeField] GameObject fishingRodPrefab;
    private GameObject fishingRodInstance;
    void Start()
    {
    }

    public FishingRod CreateFishingRod(Vector3 position) 
    {
        if (fishingRodInstance == null)
        {
            Vector3 startPosition = position;
            fishingRodInstance =  Instantiate(fishingRodPrefab, startPosition, Quaternion.identity);
        }
        return fishingRodInstance.GetComponent<FishingRod>();
    }
}