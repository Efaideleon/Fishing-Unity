using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    enum FishState
    {
        Swimming,
        Caught,
        Escaped
    }
    private BasketBall fishingRod;
    FishState fishState;
    Rigidbody rb;
    void Start()
    {
        fishState = FishState.Swimming;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (fishState == FishState.Caught)
        {
            //flop up and down
            transform.Rotate(0, 0, 10);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FishingRod"))
        {
            fishingRod = collision.gameObject.GetComponent<BasketBall>();
            fishState = FishState.Caught;
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}