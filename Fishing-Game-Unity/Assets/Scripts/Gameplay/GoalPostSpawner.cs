using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class GoalPostSpawner : NetworkBehaviour 
{
    public GameObject GoalPostPrefab;
    private GameObject _goalPostInstance;
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _goalPostInstance = Instantiate(GoalPostPrefab);
            _goalPostInstance.GetComponent<Rigidbody>().isKinematic = true;
            _goalPostInstance.transform.position = transform.position;
            _goalPostInstance.transform.rotation = transform.rotation;
            _goalPostInstance.GetComponent<NetworkObject>().Spawn();
            StartCoroutine(DisableGoalPostKinemtaicAfter1SecondCorutine());
        }
    }

    public IEnumerator DisableGoalPostKinemtaicAfter1SecondCorutine()
    {
        yield return new WaitForSeconds(1f);
        _goalPostInstance.GetComponent<Rigidbody>().isKinematic = false;
    }
}
