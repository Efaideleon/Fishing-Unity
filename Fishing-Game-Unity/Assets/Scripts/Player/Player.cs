using UnityEngine;
using FindObjectsInScene;
using Unity.Netcode;

public class Player : PlayerBase
{
    private Rigidbody _rb;
    private Vector3 _movementVector;
    private float _angle;
    private const float TORQUE_STRENGTH = 1500f;
    private BasketBall _basketBall; 
    public NetworkVariable<bool> HasBall = new(false);


    public override void OnNetworkSpawn()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
        _rb = GetComponent<Rigidbody>(); 
        _basketBall = FindInScene.BasketBall;
        if(IsLocalPlayer && FindInScene.PlayerReferenceManager.Player == null)
        {
            FindInScene.PlayerReferenceManager.Player = this;
        }
        HasBall.OnValueChanged += OnHasBallValueChanged;
    }

    void FixedUpdate()
    {
        _rb.AddRelativeForce(_movementVector * CalculateSpeed(_movementVector));
        _rb.AddRelativeTorque(transform.up * (TORQUE_STRENGTH * _angle));
    }

    public override void Turn(float angle) => _angle = angle;
    public override void Move(Vector2 direction) => _movementVector = new Vector3(direction.y, 0, -direction.x);

    private void OnHasBallValueChanged(bool oldValue, bool newValue)
    {
        if (!NetworkManager.Singleton.IsServer) return;
        if (newValue)
        {
            SetHasBallClientRpc();
        }
    }

    [ClientRpc]
    public void SetHasBallClientRpc(){
        _basketBall.SetInactive();
    }

    [ClientRpc]
    private void ThrowBallClientRpc(ThrowBallData ballData)
    {
        _basketBall.gameObject.SetActive(true); 
        _basketBall.Throw(ballData.Position, ballData.Direction);
    }

    [ServerRpc]
    private void ThrowBallServerRpc()
    {
        var client = NetworkManager.ConnectedClients[OwnerClientId];
        if (!client.PlayerObject.GetComponent<Player>().HasBall.Value) return;
        client.PlayerObject.GetComponent<Player>().HasBall.Value = false;
        var ballData = new ThrowBallData
        {
            Position = client.PlayerObject.transform.position,
            Direction = client.PlayerObject.transform.right
        };
        ThrowBallClientRpc(ballData);
    }

    private struct ThrowBallData : INetworkSerializable
    {
        public Vector3 Position;
        public Vector3 Direction;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Position);
            serializer.SerializeValue(ref Direction);
        }
    }

    public void ThrowBall()
    {
        if (!IsOwner) return;
        ThrowBallServerRpc();
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

    void OnEnable()
    {
        SubscribeToEvents();
    }

    void OnDisable()
    {
        UnsubscribeFromEvent();
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
