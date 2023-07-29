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
    }
    void Update()
    {
    }

    private void Cast()
    {
        Vector3 throwDistance = new Vector3(0, 7, 7);
        rb.AddForce(throwDistance, ForceMode.Impulse);
    }

    private void Reel()
    {
        fishingRodCatchState = FishingRodCatchState.Nothing;
        transform.position = startPosition;
        rb.isKinematic = false;
    }

    private void DestroyFish()
    {
        if (fishingRodCatchState == FishingRodCatchState.Fish)
        {
            // Destroy(fish);
            fish.SetActive(false); 
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
        if (collision.gameObject.tag == "Fish")
        {
            fishingRodCatchState = FishingRodCatchState.Fish;
            fish = collision.gameObject;
            rb.isKinematic = true;
        }

        if (collision.gameObject.tag == "Water")
        {
            rb.isKinematic = true;
        }
    }

    void ResetKinematic()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
    }
}
