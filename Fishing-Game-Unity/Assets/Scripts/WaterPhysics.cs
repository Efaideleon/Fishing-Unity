using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    void Start()
    {
    }

    private List<Rigidbody> floatingObjects = new List<Rigidbody>();
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            floatingObjects.Add(rb);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     Rigidbody rb = other.GetComponent<Rigidbody>();
    //     if (rb != null)
    //     {
    //         floatingObjects.Remove(rb);
    //     }
    // }

    private void FixedUpdate()
    {
        foreach (Rigidbody rb in floatingObjects)
        {
            ApplyBuoyancy(rb);
        }
    }

    private void ApplyBuoyancy(Rigidbody rb)
    {
        if (rb.transform.position.y < transform.position.y)
        {
            float displacementMultiplier = Mathf.Clamp01(transform.position.y - rb.transform.position.y / depthBeforeSubmerged) * displacementAmount;
            rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.VelocityChange);
        }
    }
}
