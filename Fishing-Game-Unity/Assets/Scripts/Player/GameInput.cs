using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour 
{
    private PlayerInput _playerInput;
    private InputAction _movementAction; 
    private InputAction _fishingAction;

    public delegate void EventHandler(InputAction.CallbackContext context); 
    public event EventHandler Moving;
    public event EventHandler MovementCanceled;
    public event EventHandler Throwing;
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
        _fishingAction.started += OnThrowingAction;
    }

    void OnDisable()
    {
        _movementAction.performed -= OnMoveAction;    
        _movementAction.canceled -= OnResetMoveAction;
        _fishingAction.started -= OnThrowingAction;
    }

    void OnMoveAction(InputAction.CallbackContext context)
    {   
        Moving?.Invoke(context);
    }

    void OnResetMoveAction(InputAction.CallbackContext context)
    {
        MovementCanceled?.Invoke(context);
    } 

    void OnThrowingAction(InputAction.CallbackContext context)
    {
        Throwing?.Invoke(context);
    }
}
