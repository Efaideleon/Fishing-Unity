using UnityEngine;

public class BasketBallBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Scored!");
        }
    }
}
