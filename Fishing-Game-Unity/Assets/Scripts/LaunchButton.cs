using UnityEngine;
public class LaunchButton : MonoBehaviour 
{
    private Player player;
    private PlayerReferenceManager playerReferenceManager;
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
        this.player = player.GetComponent<Player>();
    } 

    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }

    public void OnPress()
    {
        if (player != null)
        {
            player.UseFishingRod();
        }
    }
}
