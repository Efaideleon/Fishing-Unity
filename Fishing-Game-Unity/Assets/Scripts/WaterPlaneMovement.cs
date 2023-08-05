using UnityEngine;

public class WaterPlaneMovement : MonoBehaviour
{
    public float speed = 0.13f;
    public float maxDistance = 0.2f;

    private Vector3 startPosition;
    private Mesh mesh; 
    private void Start()
    {
        startPosition = transform.position;
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        // float newX = startPosition.x + Mathf.PingPong(Time.time * speed, maxDistance);
        // transform.position = new Vector3(newX, startPosition.y, startPosition.z);

        // float newY = startPosition.y + Mathf.PingPong(Time.time * speed, maxDistance);
        // transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    public float GetWaterHeight()
    {
        return transform.position.y;
    }

    public float GetWaterMeshHeight(Vector2 position)
    {
        position = transform.InverseTransformPoint(position);
        Vector3[] vertices = mesh.vertices;
        Vector3 vertex = vertices[0];
        float height = vertex.y;
        for (int i = 1; i < vertices.Length; i++)
        {
            vertex = vertices[i];
            if (Mathf.RoundToInt(vertex.x) == Mathf.RoundToInt(position.x) && Mathf.RoundToInt(vertex.z) == Mathf.RoundToInt(position.y))
            {
                height = vertex.y;
                break;
            }
        }
        return height;
    }
}

