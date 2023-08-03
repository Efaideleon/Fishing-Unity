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
        movementVector = Vector3.Normalize(new Vector3(direction.x, 0, direction.y));
    }

    private void Moving() 
    {
        Debug.Log("Speed: " + speed);
        rb.AddForce(movementVector * speed, ForceMode.Force);
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
            speed = 1/bouyancy.SurfaceAreaSideFacesUnderWater() * 10200f;
            return;
        }
        if (movementVector.x != 0)
        {
            speed = 1/bouyancy.SurfaceAreaFrontFacesUnderWater() * 12200f;
            return;
        }
        if (movementVector.z != 0)
        {
            speed = 1/bouyancy.SurfaceAreaSideFacesUnderWater() * 12000f;
            return;
        }
    }
}