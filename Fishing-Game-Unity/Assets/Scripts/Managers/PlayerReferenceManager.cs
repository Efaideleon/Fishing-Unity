using UnityEngine;
public class PlayerReferenceManager : MonoBehaviour
{
    [HideInInspector] public PlayerReferenceManager Instance;
    private static PlayerBase _player;
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
