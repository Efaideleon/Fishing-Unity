using UnityEngine;

public class BasketBallPicker : MonoBehaviour
{
    // [HideInInspector]
    public BasketBall BasketBall; 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Pick(collision);
        }
    }

    private void Pick(Collision collision)
    {
        Debug.Log("Has picked up the ball");
        BasketBall = collision.gameObject.GetComponent<BasketBall>();
        BasketBall.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        BasketBall.gameObject.GetComponent<Rigidbody>().isKinematic = true; 
        BasketBall.gameObject.SetActive(false);
    }

    public void Drop()
    {
        BasketBall = null;
    }
}
