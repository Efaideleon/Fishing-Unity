using UnityEngine;
public class PlayerReferenceManager : MonoBehaviour
{
    public PlayerReferenceManager Instance;
    private PlayerBase _player;
    public PlayerBase Player 
    {
        get { return _player; }
        set 
        {
            _player = value;
            OnPlayerSet?.Invoke(value);
        }
    }
    public delegate void PlayerSet(PlayerBase player);
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
}
