using UnityEngine;
using Unity.Netcode;
public class FishingRod : NetworkBehaviour 
{
    Rigidbody rb;
    bool isCasted = false;
    private Vector3 startPosition;
    private Vector3 holdPositionOffset;
    private GameObject fish;
    private WaterPlaneMovement waterPlane;
    [SerializeField] float throwForce;
    enum FishingRodCatchState
    {
        Fish,
        Nothing
    }
    FishingRodCatchState fishingRodCatchState;
    void Start()
    {
        holdPositionOffset = new Vector3(0, 2, 0);
        fishingRodCatchState = FishingRodCatchState.Nothing;
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        waterPlane = GameObject.Find("Water").GetComponent<WaterPlaneMovement>();
    }
    void Update()
    {
        AdjustDrag();
    }

    private void Cast(Transform user)
    {
        isCasted = true;
        Vector3 throwDirection = new(user.right.x * throwForce, throwForce, user.right.z * throwForce);
        rb.AddForce(throwDirection, ForceMode.Impulse);
    }

    private void Reel()
    {
        isCasted = false;
        transform.position = startPosition;
        rb.isKinematic = false;
    }

    private void DestroyFish()
    {
        if (fishingRodCatchState == FishingRodCatchState.Fish)
        {
            fish.SetActive(false); 
            fishingRodCatchState = FishingRodCatchState.Nothing;
        }
    }
    public void Use(Transform userTransform)
    {
        rb.isKinematic = false;
        if (isCasted)
        {
            DestroyFish();
            Reel();
        }
        else
        {
            Cast(userTransform);
        }   
    }

    public void Hold(Vector3 position)
    {
        if(!isCasted)
        {
            rb.isKinematic = true;
            transform.position = position + holdPositionOffset;
        }   
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            fishingRodCatchState = FishingRodCatchState.Fish;
            fish = collision.gameObject;
            rb.isKinematic = true;
        }
    }

    void AdjustDrag()
    {
        if (transform.position.y - 2 < waterPlane.GetWaterHeight())
        {
            rb.drag = 3f;
        }
        else
        {
            rb.drag = 0;
        }
    }
}
