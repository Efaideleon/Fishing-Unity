using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>());
        playerMovement.SetSpeed(1f);
        movementAction = playerInput.actions["Move"];
        fishingAction = playerInput.actions["CastFishingRod"];
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

    void Update()
    {
        playerMovement.Move(movementVector);
    }

    void OnMoveAction(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
    }

    void ResetMoveAction(InputAction.CallbackContext context)
    {
        movementVector = Vector2.zero;
    } 

    void OnFishingAction(InputAction.CallbackContext context)
    {
        player.UseFishingRod();
    }
}
