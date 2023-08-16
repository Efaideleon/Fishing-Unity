using UnityEngine;

public class WaterPlaneMovement : MonoBehaviour
{
    public float speed = 0.13f;
    public float maxDistance = 0.2f;

    private Material material;
    private void Start()
    {
        material = GetComponent<Renderer>().sharedMaterial;
    }

    private void Update()
    {
    }

    public float GetWaterHeight()
    {
        return transform.position.y;
    }
    public float GetWaterMeshHeight(Vector3 position)
    {
        Vector3 positionOnMeshCoordinate = transform.InverseTransformPoint(position);
        float sqrtValue = Mathf.Sqrt(Mathf.Pow(positionOnMeshCoordinate.x, 2) + Mathf.Pow(positionOnMeshCoordinate.z, 2));
        float timesFrequency = sqrtValue * material.GetFloat("_Frequency");
        float sinValue = Mathf.Sin(Time.time * material.GetFloat("_Speed") + timesFrequency);
        return sinValue/material.GetFloat("_Wave_Height");
    }
}

