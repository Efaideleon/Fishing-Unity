using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWaterVertcies : MonoBehaviour
{
    public float waveSpeed = 1f;
    public float waveHeight = 1f;
    public float waveFrequency = 1f;

    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + vertex.x * waveFrequency) * waveHeight;
            displacedVertices[i] = vertex;
        }

        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    } 
}
