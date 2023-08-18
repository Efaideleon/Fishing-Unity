using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private PlayerReferenceManager playerReferenceManager;
    private Transform target;
    [SerializeField] float smoothSpeed = 3.25f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;
    
    void Awake()
    {
        playerReferenceManager = GameObject.Find("PlayerReferenceManager").GetComponent<PlayerReferenceManager>();
    }

    void OnEnable()
    {
        playerReferenceManager.OnPlayerSet += SetTarget;
    }

    void OnDisable()
    {
        playerReferenceManager.OnPlayerSet -= SetTarget;
    }

    void SetTarget(PlayerBase player)
    {
        Debug.Log("setting target in camera follow");
        target = player.transform;
        //convert the position of the camera in the world to the position of the camera relative to the player
        offset = target.InverseTransformPoint(transform.position);
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        //convert the position of the camera in the target's coordinate system to the world coordinate system
        //the position of the camera in the target's coordinate system doesn't change
        Vector3 newPosition = target.TransformPoint(offset);

        //prevent the camera from shaking when the target moves
        var desiredPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        //move the camera to the new position
        transform.position = smoothedPosition;

        //rotate the camera to look at the target
        Vector3 relativePosition = target.position - transform.position;
        relativePosition.y = -4;
        transform.rotation = Quaternion.LookRotation(relativePosition);
    }
}
