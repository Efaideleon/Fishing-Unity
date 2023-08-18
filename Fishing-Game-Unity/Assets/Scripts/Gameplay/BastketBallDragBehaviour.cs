using UnityEngine;

public class BastketBallDragBehaviour : MonoBehaviour
{
    Rigidbody _rb;
    private WaterPlaneMovement _waterPlane;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _waterPlane = FindObjectOfType<WaterPlaneMovement>();
    } 

    void FixedUpdate()
    {
        SetAngularDrag();
    }

    void SetAngularDrag()
    {
        if (transform.position.y - 2 < _waterPlane.GetWaterHeight())
        {
            _rb.drag = 3f;  
        }
        else
        {
            _rb.drag = 0;
        }
    }
}
