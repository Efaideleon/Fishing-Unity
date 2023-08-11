using UnityEngine;
using Unity.Netcode;
public class Player : NetworkBehaviour 
{
    private FishingRod fishingRod;
    private FishingRodFactory fishingRodFactory;
    private PlayerReferenceManager playerReferenceManager;

    void Start()
    {
        if (!IsOwner) return;
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
        // if (!IsOwner) return;
        // if (fishingRod != null)
        //     fishingRod.Hold(transform.position);
    }

    public void UseFishingRod()
    {
        fishingRod.Use(transform);
    }
}
