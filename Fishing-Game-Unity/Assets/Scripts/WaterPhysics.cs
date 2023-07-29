using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    public float buoyancyForce = 2.0f;
    public float dragForce = 0.5f;
    public float waterLevel = 0.0f;

    private List<Rigidbody> floatingObjects = new List<Rigidbody>();
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            floatingObjects.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            floatingObjects.Remove(rb);
        }
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody rb in floatingObjects)
        {
            ApplyBuoyancy(rb);
            ApplyDrag(rb);
        }
    }

    private void ApplyBuoyancy(Rigidbody rb)
    {
        float objectHeight = rb.transform.position.y;
        if (objectHeight < waterLevel)
        {
            float submergedVolume = waterLevel - objectHeight;
            float buoyancy = submergedVolume * buoyancyForce;
            rb.AddForce(Vector3.up * buoyancy, ForceMode.Acceleration);
        }
    }

    private void ApplyDrag(Rigidbody rb)
    {
        float objectHeight = rb.transform.position.y;
        if (objectHeight < waterLevel)
        {
            rb.drag = dragForce;
        }
        else
        {
            rb.drag = 0;
        }
    }
}
