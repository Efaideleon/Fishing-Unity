using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    Rigidbody rb;
    bool isCasted = false;
    private Vector3 startPosition;
    private Vector3 holdPositionOffset;
    private GameObject fish;
    private WaterPlaneMovement waterPlane;
    [SerializeField] Vector3 throwForce;
    enum FishingRodCatchState
    {
        Fish,
        Nothing
    }
    FishingRodCatchState fishingRodCatchState;
    void Start()
    {
        holdPositionOffset = new Vector3(0, 2, 2);
        fishingRodCatchState = FishingRodCatchState.Nothing;
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        waterPlane = GameObject.Find("Water").GetComponent<WaterPlaneMovement>();
    }
    void Update()
    {
        AdjustDrag();
    }

    private void Cast()
    {
        rb.AddForce(throwForce, ForceMode.Impulse);
    }

    private void Reel()
    {
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

    public void Use(Vector3 position)
    {
        startPosition = position + holdPositionOffset;
        ResetKinematic();
        if (isCasted)
        {
            DestroyFish();
            Reel();
            isCasted = false;
        }
        else
        {
            Cast();
            isCasted = true;
        }   
    }

    public void Hold(Vector3 position)
    {
        if(!isCasted)
        {
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

    void ResetKinematic()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
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
