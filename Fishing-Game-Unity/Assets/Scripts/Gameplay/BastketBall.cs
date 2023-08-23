using UnityEngine;
using Unity.Netcode;

public class BasketBall : MonoBehaviour, IBasketBall
{
    Rigidbody _rb;
    private float _throwForce = 20f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = false;
    }
    public void Throw(Vector3 position, Vector3 direction)
    {
        gameObject.SetActive(true);
        _rb.isKinematic = false;
        Vector3 ballStartingPositionOffset = new(0, 3, 0);
        transform.position = position + ballStartingPositionOffset;
        Vector3 throwDirection = new(direction.x * _throwForce, _throwForce, direction.z * _throwForce);
        _rb.AddForce(throwDirection, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        if (!NetworkManager.Singleton.IsServer) return; 
        collider.GetComponent<Player>().HasBall.Value = true;
    }
    
    public void SetInactive()
    {
        gameObject.SetActive(false);
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true; 
    }
}
