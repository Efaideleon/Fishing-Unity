using Unity.Netcode;
using UnityEngine;
public class PlayerMovement : NetworkBehaviour, IMoveable
{
    private Rigidbody rb;
    private float speed;
    private Vector3 movementVector;
    private readonly float torqueStrength = 5000f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Debug.Log("Rigidbody " + rb);
    }

    void FixedUpdate()
    {
        CalculateSpeed();
        Moving();

        if (transform.position.y < - 2)
        {
            transform.SetPositionAndRotation(new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 0));
        }
    }

    public void UpdateMoveVector(Vector2 direction)
    {
        /*
        * local transform.x is forward/backward
        * local transform.z is right/left 
        */
        movementVector = Vector3.Normalize(new Vector3(direction.y, 0, -direction.x));
    }

    private void Moving() 
    {
        Debug.Log("a Movement vector: " + movementVector + " " + NetworkManager.LocalClientId);
        rb.AddRelativeTorque(transform.up * (torqueStrength * .6f * -movementVector.z));
    }

    public void Rotate(float angle)
    {
        float torqueStrength = 1000f;
        rb.AddRelativeTorque(transform.up * (torqueStrength * .6f * angle));
    }

    private void CalculateSpeed()
    {
        if (movementVector.x !=0)
            speed = 6000f;
        else if (movementVector.z != 0)
            speed = 200f;
        else if (movementVector.x != 0 && movementVector.z != 0)
            speed = 3000f;
        else if (movementVector.x == 0 && movementVector.z == 0)
             speed = 0f;
    }

}