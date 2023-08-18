using UnityEngine;
using FindObjectsInScene;

public class Player : PlayerBase
{
    private Rigidbody _rb;
    private Vector3 _movementVector;
    private float _angle;
    private const float TORQUE_STRENGTH = 1500f;
    private BasketBallPicker _basketBallPicker;
    void OnEnable()
    {
        SubscribeToEvents();
    }

    void OnDisable()
    {
        UnsubscribeFromEvent();
    }

    void Start()
    {
        if (!IsOwner) transform.position = new Vector3(30,0, 10);
        transform.rotation = Quaternion.Euler(0, -90, 0);
        _rb = GetComponent<Rigidbody>(); 
        _basketBallPicker = GetComponentInChildren<BasketBallPicker>();

        if(IsLocalPlayer && FindInScene.PlayerReferenceManager.Player == null)
        {
            FindInScene.PlayerReferenceManager.Player = this;
        }
    }

    void FixedUpdate()
    {
        _rb.AddRelativeForce(_movementVector * CalculateSpeed(_movementVector));
        _rb.AddRelativeTorque(transform.up * (TORQUE_STRENGTH * _angle));
    }

    public override void Turn(float angle) => _angle = angle;
    public override void Move(Vector2 direction) => _movementVector = new Vector3(direction.y, 0, -direction.x);

    public void ThrowBall()
    {
        if (_basketBallPicker.BasketBall == null) return;
        Vector3 ballStartingPositionOffset = new(0, 2, 0);
        _basketBallPicker.BasketBall.SetWorldPosition(transform.position + ballStartingPositionOffset);
        _basketBallPicker.BasketBall.Throw(transform.right);
        _basketBallPicker.Drop();
    }

    private void OnMove(Vector2 direction)
    {
        Move(direction);
        Turn(-_movementVector.z);
    }

    private void OnMovementCancel()
    {
        Move(Vector2.zero);
        Turn(0);
    }

    private void SubscribeToEvents()
    {
        FindInScene.GameInput.OnMoved += context => OnMove(context.ReadValue<Vector2>());
        FindInScene.GameInput.OnMovementCanceled += context => OnMovementCancel();
        FindInScene.GameInput.OnFishing += context => ThrowBall();
        FindInScene.Wheel.OnRotate += Turn;
        FindInScene.Pedal.OnPress += Move; 
        FindInScene.BackButton.OnBack += Move;
        FindInScene.LaunchButton.OnLaunch += ThrowBall;
    }

    private void UnsubscribeFromEvent()
    {
        FindInScene.GameInput.OnMoved -= context => OnMove(context.ReadValue<Vector2>());
        FindInScene.GameInput.OnMovementCanceled -= context => OnMovementCancel();
        FindInScene.GameInput.OnFishing -= context => ThrowBall();
        FindInScene.Wheel.OnRotate -= Turn;
        FindInScene.Pedal.OnPress -= Move;
        FindInScene.BackButton.OnBack -= Move;
        FindInScene.LaunchButton.OnLaunch -= ThrowBall;
    }
}
