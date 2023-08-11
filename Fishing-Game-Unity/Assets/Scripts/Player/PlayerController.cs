using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    private Player player;
    private PlayerInput playerInput;
    private InputAction movementAction;
    private InputAction fishingAction;
    private IMoveable playerMovement;
    private Vector2 movementVector;
    void Awake()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();  
        movementAction = playerInput.actions["Move"];
        fishingAction = playerInput.actions["CastFishingRod"];
        Debug.Log("Setting controllers");
    }

    void OnEnable()
    {
        movementAction.performed += OnMoveAction;    
        movementAction.canceled += ResetMoveAction;
        fishingAction.started += OnFishingAction;
    }

    void OnDisable()
    {
        movementAction.performed -= OnMoveAction;    
        movementAction.canceled -= ResetMoveAction;
        fishingAction.started -= OnFishingAction;
    }

    void OnMoveAction(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
        playerMovement.UpdateMoveVector(movementVector);
    }

    void ResetMoveAction(InputAction.CallbackContext context)
    {
        Debug.Log(OwnerClientId + " " + NetworkManager.Singleton.LocalClientId);
        movementVector = Vector2.zero;
        playerMovement.UpdateMoveVector(movementVector);
    } 

    void OnFishingAction(InputAction.CallbackContext context)
    {
        player.UseFishingRod();
    }
}
