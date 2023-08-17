using UnityEngine;

public class BasketBallPicker : MonoBehaviour
{
    [HideInInspector]
    public BasketBall BasketBall { get; set; }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BasketBall = collision.gameObject.GetComponent<BasketBall>();
            BasketBall.gameObject.transform.SetParent(transform);
            BasketBall.gameObject.transform.localPosition = new Vector3(0, 2f, 0);
            BasketBall.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
