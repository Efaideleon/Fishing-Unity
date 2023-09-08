using UnityEngine;
using FindObjectsInScene;
using Unity.Netcode;

public class Player : PlayerBase
{
    private Rigidbody _rb;
    private float _speed; 
    private float _angle;
    private const float TORQUE_STRENGTH = 2000f;
    private ThrowItem _throwItem; 
    public NetworkVariable<bool> HasBall = new(false);
    private Vector3 _localForwardDirection;

    public override void OnNetworkSpawn()
    {
        _localForwardDirection = Vector3.right;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        _rb = GetComponent<Rigidbody>(); 
        _throwItem = FindInScene.BasketBall;
        if(IsLocalPlayer && FindInScene.PlayerReferenceManager.Player == null)
        {
            FindInScene.PlayerReferenceManager.Player = this;
        }
        HasBall.OnValueChanged += OnHasBallValueChanged;
    }

    void FixedUpdate()
    {
        _rb.AddRelativeForce(_localForwardDirection * CalculateForce(_speed));
        _rb.AddRelativeTorque(transform.up * (TORQUE_STRENGTH * _angle));
    }

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
        _throwItem.SetInactive();
    }

    [ClientRpc]
    private void ThrowBallClientRpc(ThrowBallData ballData)
    {
        _throwItem.gameObject.SetActive(true); 
        _throwItem.OwnerId = ballData.OwnerId;
        _throwItem.Throw(ballData.Position, ballData.Direction);
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
            Direction = client.PlayerObject.transform.right,
            OwnerId = client.ClientId
        };
        ThrowBallClientRpc(ballData);
    }

    private struct ThrowBallData : INetworkSerializable
    {
        public Vector3 Position;
        public Vector3 Direction;
        public ulong OwnerId;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Position);
            serializer.SerializeValue(ref Direction);
            serializer.SerializeValue(ref OwnerId);
        }
    }

    public override void SetAngle(float angle) => _angle = angle;
    public override void SetSpeed(float speed) => _speed = speed;

    public void ThrowBall()
    {
        if (!IsOwner) return;
        ThrowBallServerRpc();
    }

    private void Move(Vector2 movementDirection)
    {
        SetAngle(movementDirection.x);
        SetSpeed(movementDirection.y);
    }

    private void CancelMovement()
    {
        SetAngle(0);
        SetSpeed(0);
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
        FindInScene.GameInput.Moving += context => Move(context.ReadValue<Vector2>());
        FindInScene.GameInput.MovementCanceled += context => CancelMovement();
        FindInScene.GameInput.Throwing += context => ThrowBall();
        FindInScene.Wheel.Rotating += SetAngle;
        FindInScene.Pedal.Pressing += SetSpeed; 
        FindInScene.BackButton.Backing += SetSpeed;
        FindInScene.LaunchButton.Launched += ThrowBall;
    }

    private void UnsubscribeFromEvent()
    {
        FindInScene.GameInput.Moving -= context => Move(context.ReadValue<Vector2>());
        FindInScene.GameInput.MovementCanceled -= context => CancelMovement();
        FindInScene.GameInput.Throwing -= context => ThrowBall();
        FindInScene.Wheel.Rotating -= SetAngle;
        FindInScene.Pedal.Pressing -= SetSpeed;
        FindInScene.BackButton.Backing -= SetSpeed;
        FindInScene.LaunchButton.Launched -= ThrowBall;
    }
}
