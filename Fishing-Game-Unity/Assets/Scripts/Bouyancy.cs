using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    private readonly float fuildDensity = 45;
    [SerializeField] Collider objectCollider;
    [SerializeField] Rigidbody rb;
    private float width;
    private float height;
    private float depth; 
    private WaterPlaneMovement waterPlaneMovement;
    [SerializeField] float numberOfFloaters;
    void Start()
    {  
        width = objectCollider.bounds.size.x;
        height = objectCollider.bounds.size.y;
        depth = objectCollider.bounds.size.z; 
        waterPlaneMovement = GameObject.Find("Water").GetComponent<WaterPlaneMovement>();
    }

    void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity/numberOfFloaters, transform.position, ForceMode.Acceleration);
        ApplyBuoyancy();
    }

    void ApplyBuoyancy()
    {
        if (IsUnderWater()) 
        {
            Vector3 buoyancyForce = fuildDensity * VolumeOfPushedOutFluid() * -Physics.gravity;
            rb.AddForceAtPosition(buoyancyForce, transform.position, ForceMode.Force);
        }
    }

    public float VolumeOfPushedOutFluid()
    {
        return (width * GetHeightSumberged() * depth)/numberOfFloaters;
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
        //change name from ship to object
        float objectBottomPosition = transform.position.y;
        return Mathf.Clamp(Mathf.Abs(objectBottomPosition - waterPlaneMovement.GetWaterMeshHeight(transform.position)), 0, height);
    }

    public bool IsUnderWater()
    {
        float waveMeshHeight = waterPlaneMovement.GetWaterMeshHeight(transform.position);
        return transform.position.y < waveMeshHeight;
    }
}
