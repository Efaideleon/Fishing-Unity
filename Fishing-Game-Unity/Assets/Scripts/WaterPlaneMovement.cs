using UnityEngine;

public class WaterPlaneMovement : MonoBehaviour
{
    public float speed = 0.13f;
    public float maxDistance = 0.2f;

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

    public float GetWaterHeight()
    {
        return transform.position.y;
    }
}

