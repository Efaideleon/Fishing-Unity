using UnityEngine;
public class UIPedal : MonoBehaviour
{
    private PlayerReferenceManager playerReferenceManager;
    private PlayerMovement player;

    private void Awake()
    {
        try{
            playerReferenceManager = GameObject.Find("PlayerReferenceManager").GetComponent<PlayerReferenceManager>();
        }
        catch{
            Debug.Log("PlayerReferenceManager not found");
        }
    }

    void OnEnable()
    {
        playerReferenceManager.OnPlayerSet += SetPlayer;
    }

    void OnDisable()
    {
        playerReferenceManager.OnPlayerSet -= SetPlayer;
    }

    void SetPlayer(GameObject player)
    {
        this.player = player.GetComponent<PlayerMovement>();
    }
    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }

    public void OnPress(Vector2 movementVector)
    {
        if (player != null)
        {
            player.UpdateMoveVector(movementVector);
        }
    }
}
