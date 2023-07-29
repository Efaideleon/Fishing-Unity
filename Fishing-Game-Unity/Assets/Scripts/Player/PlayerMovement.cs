using UnityEngine;
public class PlayerMovement : IMoveable
{
    private Rigidbody rb;
    private float speed = 5f;
    public PlayerMovement(Rigidbody rb)
    {
        this.rb = rb;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Move(Vector2 direction)
    {
        Vector3 movementVector = new Vector3(direction.x, 0, direction.y);
        rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);
    }
}