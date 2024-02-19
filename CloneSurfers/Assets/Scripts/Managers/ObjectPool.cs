using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    List<GameObject> _pooledMaps = new List<GameObject>();
    List<GameObject> _pooledCoins = new List<GameObject>();
    List<GameObject> _pooledCoinParticles = new List<GameObject>();

    int _coinCounter = 0;
    int _coinPoolAmount = 90;

    int _coinParticleCounter = 0;
    int _coinParticlePoolAmount = 5;

    [SerializeField] GameObject[] _mapPrefabs;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] GameObject _coinParticlePrefab;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        for (int i = 0; i < _mapPrefabs.Length; i++)
        {
            GameObject map = Instantiate(_mapPrefabs[i]);
            map.SetActive(false);
            _pooledMaps.Add(map);
        }
        for (int i = 0; i < _coinPoolAmount; i++)
        {
            GameObject coin = Instantiate(_coinPrefab);
            coin.SetActive(false);
            _pooledCoins.Add(coin);
        }
        for (int i = 0; i < _coinParticlePoolAmount; i++)
        {
            GameObject particle = Instantiate(_coinParticlePrefab);
            particle.SetActive(false);
            _pooledCoinParticles.Add(particle);
        }
    }

    public GameObject GetNewMap()
    {
        int newMap = Random.Range(0, _mapPrefabs.Length);
        if (!_pooledMaps[newMap].activeSelf)
            return _pooledMaps[newMap];

        return GetNewMap();
    }
    public GameObject GetNewCoin()
    {
        if (_coinCounter >= _coinPoolAmount)
            _coinCounter = 0;

        if (!_pooledCoins[_coinCounter].activeSelf)
            return _pooledCoins[_coinCounter++];

        return GetNewCoin();
    }
    public GameObject GetNewParticle()
    {
        if (_coinParticleCounter >= _coinParticlePoolAmount)
            _coinParticleCounter = 0;

        return _pooledCoinParticles[_coinParticleCounter++];
    }
}
