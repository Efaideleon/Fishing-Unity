using UnityEngine;
public class FishingRodFactory : MonoBehaviour
{
    [SerializeField] GameObject fishingRodPrefab;
    private GameObject fishingRodInstance;
    void Start()
    {
    }

    public BasketBall CreateFishingRod(Vector3 position) 
    {
        if (fishingRodInstance == null)
        {
            Vector3 startPosition = position;
            fishingRodInstance =  Instantiate(fishingRodPrefab, startPosition, Quaternion.identity);
        }
        return fishingRodInstance.GetComponent<BasketBall>();
    }
}