using UnityEngine;

public class PlayerReferenceManager : MonoBehaviour
{

    public PlayerReferenceManager Instance;
    private GameObject player;

    public delegate void PlayerSet(GameObject player);
    public event PlayerSet OnPlayerSet;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    public void SetPlayer(GameObject player)
    {
        this.player = player;
        OnPlayerSet?.Invoke(player);
    }
}
