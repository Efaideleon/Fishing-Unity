using UnityEngine;
using Unity.Netcode;

public class BasketBall : NetworkBehaviour, IBasketBall
{
    Rigidbody _rb;
    private float _throwForce = 20f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Throw(Vector3 direction)
    {
        gameObject.SetActive(true);
        _rb.isKinematic = false;
        Vector3 throwDirection = new(direction.x * _throwForce, _throwForce, direction.z * _throwForce);
        _rb.AddForce(throwDirection, ForceMode.Impulse);
    }

    public void SetWorldPosition(Vector3 position)
    {
        transform.position = position;
    }
}
