using UnityEngine;
using UnityEngine.UI;
public class UIWheel : MonoBehaviour
{
    private RawImage wheelImage;
    [SerializeField] private PlayerMovement playerMovement;
    void Awake()
    {
        wheelImage = GetComponent<RawImage>();
    }

    public void Rotate(float angle)
    {
        wheelImage.rectTransform.Rotate(0,0,angle);
        playerMovement.Rotate(-angle);
    } 

    public RectTransform GetRectTransform()
    {
        return wheelImage.rectTransform;
    }
}
