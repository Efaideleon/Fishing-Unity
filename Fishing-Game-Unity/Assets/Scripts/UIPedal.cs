using UnityEngine;

public class UIPedal : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }

    public void OnPress(Vector2 movementVector)
    {
        playerMovement.UpdateMoveVector(movementVector);
    }
}
