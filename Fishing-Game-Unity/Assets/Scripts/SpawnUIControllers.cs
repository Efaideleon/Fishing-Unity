using UnityEngine;
using Unity.Netcode;

public class SpawnUIControllers : NetworkBehaviour
{
    [SerializeField] GameObject wheelPrefab;
    [SerializeField] GameObject pedalPrefab;
    [SerializeField] GameObject launchButtonPrefab;
    [SerializeField] GameObject canvas;

    private GameObject wheel;
    private GameObject pedal;
    private GameObject launchButton;
    private GameObject player;

    void Start()
    {
        wheel = Instantiate(wheelPrefab, canvas.transform);
        pedal = Instantiate(pedalPrefab, canvas.transform);
        launchButton = Instantiate(launchButtonPrefab, canvas.transform);
    }
}
