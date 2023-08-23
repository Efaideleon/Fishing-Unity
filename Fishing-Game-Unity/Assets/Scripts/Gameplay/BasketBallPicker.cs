using UnityEngine;
using Unity.Netcode;
public class BasketBallPicker : MonoBehaviour  
{
    // [HideInInspector]
    public BasketBall BasketBall; 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BasketBall>(out var component))
        {
            Pick(component);
        }
    }

    private void Pick(BasketBall component)
    {
        Debug.Log("Has picked up the ball");
        if (!NetworkManager.Singleton.IsServer) return;
        Debug.Log("The server has picked up the ball");
        BasketBall = component; 
        BasketBall.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        BasketBall.gameObject.GetComponent<Rigidbody>().isKinematic = true; 
        BasketBall.gameObject.SetActive(false);
    }

    public void Drop()
    {
        BasketBall = null;
    }
}
