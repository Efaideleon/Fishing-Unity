using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    private readonly float fuildDensity = 45;
    private Collider objectCollider;
    private Rigidbody rb;
    private float width;
    private float height;
    private float depth; 
    private WaterPlaneMovement waterPlaneMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
        width = objectCollider.bounds.size.x;
        height = objectCollider.bounds.size.y;
        depth = objectCollider.bounds.size.z; 
        waterPlaneMovement = GameObject.Find("Water").GetComponent<WaterPlaneMovement>();
    }

    void FixedUpdate()
    {
        ApplyBuoyancy();
    }

    void ApplyBuoyancy()
    {
        if (transform.position.y - height/2 < waterPlaneMovement.GetWaterHeight()) 
        {
            Vector3 buoyancyForce = fuildDensity * VolumeOfPushedOutFluid() * -Physics.gravity;
            rb.AddForce(buoyancyForce, ForceMode.Force);
        }
    }

    public float VolumeOfPushedOutFluid()
    {
        float shipBottomPosition = transform.position.y - height/2;
        float amountSubmerged = Mathf.Clamp(Mathf.Abs(shipBottomPosition - waterPlaneMovement.GetWaterHeight()), 0, height);
        return (width * amountSubmerged * depth);
    }
}
