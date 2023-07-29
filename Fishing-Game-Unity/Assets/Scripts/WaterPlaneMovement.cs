using UnityEngine;

public class WaterPlaneMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float maxDistance = 10.0f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newX = startPosition.x + Mathf.PingPong(Time.time * speed, maxDistance);
        transform.position = new Vector3(newX, startPosition.y, startPosition.z);

        float newY = startPosition.y + Mathf.PingPong(Time.time * speed, maxDistance);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}

