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
        if (IsUnderWater()) 
        {
            Vector3 buoyancyForce = fuildDensity * VolumeOfPushedOutFluid() * -Physics.gravity;
            rb.AddForce(buoyancyForce, ForceMode.Force);
        }
    }

    public float VolumeOfPushedOutFluid()
    {
        return width * GetHeightSumberged() * depth;
    }

    public float SurfaceAreaFrontFacesUnderWater()
    {
        return depth * GetHeightSumberged();
    }

    public float SurfaceAreaSideFacesUnderWater()
    {
        return width * GetHeightSumberged();
    }

    private float GetHeightSumberged()
    {
        float shipBottomPosition = transform.position.y - height/2;
        return Mathf.Clamp(Mathf.Abs(shipBottomPosition - waterPlaneMovement.GetWaterHeight()), 0, height);
    }

    public bool IsUnderWater()
    {
        Debug.Log(waterPlaneMovement.GetWaterMeshHeight(new Vector2(transform.position.x, transform.position.z))); 
        return transform.position.y - height/2 < waterPlaneMovement.GetWaterHeight();
    }
}
