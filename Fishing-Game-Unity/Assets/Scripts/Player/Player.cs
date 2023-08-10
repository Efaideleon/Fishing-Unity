using UnityEngine;

public class Player : MonoBehaviour
{
    private FishingRod fishingRod;
    private FishingRodFactory fishingRodFactory;
    private PlayerReferenceManager playerReferenceManager;

    void Start()
    {

        transform.rotation = Quaternion.Euler(0, -90, 0);
        try{
            playerReferenceManager = GameObject.Find("PlayerReferenceManager").GetComponent<PlayerReferenceManager>();
            playerReferenceManager.Instance.SetPlayer(gameObject);
        }
        catch{
            Debug.Log("PlayerReferenceManager not found");
        }

        fishingRodFactory = GameObject.Find("FishingRodFactory").GetComponent<FishingRodFactory>();
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
