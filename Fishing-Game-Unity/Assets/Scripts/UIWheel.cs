using UnityEngine;
using UnityEngine.UI;
public class UIWheel : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public void Rotate(float angle)
    {
        GetComponent<RectTransform>().Rotate(0,0,angle);
        playerMovement.Rotate(-angle);
    } 

    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }
}
