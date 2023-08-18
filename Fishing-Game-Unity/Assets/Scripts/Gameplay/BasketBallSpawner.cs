using UnityEngine;
public class BasketBallSpawner: MonoBehaviour
{
    [SerializeField] private BasketBall _basketBallPrefab;
    private static BasketBall _basketBallInstance;
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