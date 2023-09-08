using UnityEngine;
using FindObjectsInScene;
using Unity.Netcode;

public class Hoop : NetworkBehaviour
{
    struct PlayerData : INetworkSerializable
    {
        public ulong ClientId;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ClientId);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out ThrowItem throwItem))
        {
            if (!NetworkManager.Singleton.IsServer) return;
            PlayerData playerData = new PlayerData
            {
                ClientId = throwItem.OwnerId
            };
            UpdateScoreServerRpc(playerData);
        }
    }

    [ServerRpc]
    private void UpdateScoreServerRpc(PlayerData playerData) 
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new[] { playerData.ClientId }
            }
        };
        UpdateScoreClientRpc(clientRpcParams);
    }

    [ClientRpc]
    private void UpdateScoreClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("updating score in client rpc");
        FindInScene.GameManager.UpdateScore();
    }
}
