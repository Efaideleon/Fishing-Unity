using UnityEngine;
public class UIWheel : MonoBehaviour 
{
    private PlayerReferenceManager playerReferenceManager;
    private PlayerMovement player;

    void Awake()
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
    
    public void Rotate(float angle)
    {
        if (player != null)
        {
            GetComponent<RectTransform>().Rotate(0,0,angle);
            player.Rotate(-angle);
        }
    } 

    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }
}
