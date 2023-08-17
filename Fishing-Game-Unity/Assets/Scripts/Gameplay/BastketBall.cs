using UnityEngine;
using Unity.Netcode;

public class BasketBall : NetworkBehaviour, IBasketBall
{
    Rigidbody _rb;
    private float _throwForce = 40f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Throw(Vector3 direction)
    {
        Debug.Log("Throwing ball"); 
        Vector3 throwDirection = new(direction.x * _throwForce, _throwForce, direction.z * _throwForce);
        _rb.AddForce(throwDirection, ForceMode.Impulse);
    }
}
