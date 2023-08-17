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
        if (!IsOwner) return;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        _rb = GetComponent<Rigidbody>(); 
        FindInScene.PlayerReferenceManager.Player = this;
        _basketBallPicker = GetComponentInChildren<BasketBallPicker>();

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
        _basketBallPicker.BasketBall.Throw(transform.right);
        _basketBallPicker.BasketBall = null;
    }

    private void SubscribeToEvents()
    {
        FindInScene.GameInput.OnMoved += context => Move(context.ReadValue<Vector2>());
        FindInScene.GameInput.OnMoved += context => Turn(-_movementVector.z);
        FindInScene.GameInput.OnMovementCanceled += context => Move(Vector2.zero);
        FindInScene.GameInput.OnMovementCanceled += context => Turn(0);
        FindInScene.Wheel.OnRotate += Turn;
        FindInScene.Pedal.OnPress += Move; 
        FindInScene.BackButton.OnBack += Move;
        FindInScene.LaunchButton.OnLaunch += ThrowBall;
    }

    private void UnsubscribeFromEvent()
    {
        FindInScene.GameInput.OnMoved -= context => Move(context.ReadValue<Vector2>());
        FindInScene.GameInput.OnMoved -= context => Turn(-_movementVector.z);
        FindInScene.GameInput.OnMovementCanceled -= context => Move(Vector2.zero);
        FindInScene.GameInput.OnMovementCanceled -= context => Turn(0);
        FindInScene.Wheel.OnRotate -= Turn;
        FindInScene.Pedal.OnPress -= Move;
        FindInScene.BackButton.OnBack -= Move;
        FindInScene.LaunchButton.OnLaunch -= ThrowBall;
    }
}
