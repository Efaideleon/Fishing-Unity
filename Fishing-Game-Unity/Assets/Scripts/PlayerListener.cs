using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{
    private PlayerReferenceManager _playerReferenceManager;
    private IPlayer _player;
    public IPlayer Player => _player;

    void Awake()
    {
        try{
            _playerReferenceManager = GameObject.Find("PlayerReferenceManager").GetComponent<PlayerReferenceManager>();
        }
        catch{
            Debug.Log("PlayerReferenceManager not found");
        }
    }

    void OnEnable()
    {
        _playerReferenceManager.OnPlayerSet += player => { _player = player; };
    }

    void OnDisable()
    {
        _playerReferenceManager.OnPlayerSet -= player => { _player = player; };
    }
}
