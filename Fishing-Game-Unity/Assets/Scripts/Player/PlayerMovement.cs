using UnityEngine;
public class PlayerMovement : MonoBehaviour, IMoveable
{
    private Rigidbody rb;
    private float speed;
    private Vector3 movementVector;
    private Bouyancy bouyancy;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bouyancy = GetComponent<Bouyancy>();
    }

    void FixedUpdate()
    {
        if (bouyancy.IsUnderWater())
        {
            Moving();
            CalcuateSpeed();
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
        Debug.Log("Speed: " + speed);
        rb.AddRelativeForce(movementVector * speed);
        rb.AddRelativeTorque(transform.up * (speed * .6f * -movementVector.z));
    }

    private void CalcuateSpeed()
    {
        if (movementVector == Vector3.zero)
        {
            speed = 0;
            return;
        }
        if (movementVector.x != 0 && movementVector.z != 0)
        {
            speed = 1/bouyancy.SurfaceAreaSideFacesUnderWater() * 2200f;
            return;
        }
        if (movementVector.x != 0)
        {
            speed = 1/bouyancy.SurfaceAreaFrontFacesUnderWater() * 12200f;
            return;
        }
        if (movementVector.z != 0)
        {
            speed = 1/bouyancy.SurfaceAreaSideFacesUnderWater() * 3000f;
            return;
        }
    }
}