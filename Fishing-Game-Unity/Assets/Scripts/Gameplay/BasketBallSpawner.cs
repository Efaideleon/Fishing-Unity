using UnityEngine;
public class BasketBallSpawner: MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private ThrowItem _basketBallPrefab;
    private static ThrowItem _basketBallInstance;
    private Vector3 _basketBallSpawnPosition;
    void Start()
    {
        _basketBallSpawnPosition = new Vector3(0, 2, 20);
        SpawnBasketBall();
    }

    private void SpawnBasketBall() 
    {
        if (_basketBallInstance == null)
        {
            _basketBallInstance =  Instantiate(_basketBallPrefab, _basketBallSpawnPosition, Quaternion.identity);
        }
    }
}