using UnityEngine;

public class MapSetup : MonoBehaviour
{
    public bool CanAddNewMap { get; private set; }

    [SerializeField] Transform[] _coinPositions;
    [SerializeField] int _maxCoinOnMap;
    bool[] _isFull;


    public void OnCreate()
    {

    }
    public void OnActivate(ObjectPool pool)
    {
        if (_isFull == null)
            _isFull = new bool[_coinPositions.Length];

        CanAddNewMap = true;
        SetCoins(pool);
        
        GetComponent<IMapEvent>().OnActivate();
    }
    public void OnDeactivate()
    {
        if (_isFull == null)
            _isFull = new bool[_coinPositions.Length];
        for (int i = 0; i < _isFull.Length; i++)
        {
            _isFull[i] = false;
        }
        GetComponent<IMapEvent>().OnDeactivate();
    }
    public void MapCreated()
    {
        CanAddNewMap = false;
    }

    void SetCoins(ObjectPool pool)
    {
        for (int i = 0; i < _maxCoinOnMap; i++)
        {
            GameObject coin = pool.GetNewCoin();
            coin.transform.position = CoinPosition().position;
            coin.SetActive(true);
        }
    }

    Transform CoinPosition()
    {
        int rand = Random.Range(0, _coinPositions.Length);
        if (!_isFull[rand])
        {
            _isFull[rand] = true;
            return _coinPositions[rand];
        }
        return CoinPosition();
    }
}
