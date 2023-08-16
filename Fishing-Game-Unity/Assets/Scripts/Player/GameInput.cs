using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour 
{
    private PlayerInput _playerInput;
    private InputAction _movementAction; 
    private InputAction _fishingAction;

    public delegate void EventHandler(InputAction.CallbackContext context); 
    public event EventHandler OnMoved;
    public event EventHandler OnMovementCanceled;
    public event EventHandler OnFishing;
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementAction = _playerInput.actions["Move"];
        _fishingAction = _playerInput.actions["CastFishingRod"];
    }

    void OnEnable()
    {
        _movementAction.performed += OnMoveAction;    
        _movementAction.canceled += OnResetMoveAction; 
        _fishingAction.started += OnFishingAction;
    }

    void OnDisable()
    {
        _movementAction.performed -= OnMoveAction;    
        _movementAction.canceled -= OnResetMoveAction;
        _fishingAction.started -= OnFishingAction;
    }

    void OnMoveAction(InputAction.CallbackContext context)
    {   
        OnMoved?.Invoke(context);
    }

    void OnResetMoveAction(InputAction.CallbackContext context)
    {
        OnMovementCanceled?.Invoke(context);
    } 

    void OnFishingAction(InputAction.CallbackContext context)
    {
        OnFishing?.Invoke(context);
    }
}
