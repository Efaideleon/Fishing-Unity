using UnityEngine;
public class PlayerMovement : MonoBehaviour, IMoveable
{
    private Rigidbody rb;
    private float speed;
    private Vector3 movementVector;
    private readonly float torqueStrength = 7000f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Moving();
        CalcuateSpeed();
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
        rb.AddRelativeForce(movementVector * speed);
        rb.AddRelativeTorque(transform.up * (torqueStrength * .6f * -movementVector.z));
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
            speed = 2200f;
            return;
        }
        if (movementVector.x != 0)
        {
            speed = 5200f;
            return;
        }
        if (movementVector.z != 0)
        {
            speed = 2200f;
            return;
        }
    }
}