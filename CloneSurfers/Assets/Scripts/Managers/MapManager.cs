using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject _tempMap;
    [SerializeField] GameObject[] _movingMaps;

    [SerializeField] ObjectPool _pool;

    [SerializeField] float _speed;
    [SerializeField] float _creationPosZ;

    Vector3 _mapStartPos = new Vector3(15.7f, -9f, 6.5f);
    Vector3 _newZPos = new Vector3(0, 0, 14.85f);


    private void Start()
    {
        InitializeStartMap();
    }

     void InitializeStartMap()
    {
        GameObject newMap1 = _pool.GetNewMap();
        newMap1.transform.position = _mapStartPos;
        newMap1.SetActive(true);

        GameObject newMap2 = _pool.GetNewMap();
        newMap2.transform.position = newMap1.transform.position + _newZPos;
        newMap2.SetActive(true);

        GameObject newMap3 = _pool.GetNewMap();
        newMap3.transform.position = newMap2.transform.position + _newZPos;
        newMap3.SetActive(true);

        GameObject newMap4 = _pool.GetNewMap();
        newMap4.transform.position = newMap3.transform.position + _newZPos;
        newMap4.SetActive(true);

        _movingMaps = new GameObject[] { newMap1, newMap2, newMap3, newMap4 };

        for (int i = 0; i < _movingMaps.Length; i++)
            _movingMaps[i].GetComponent<MapSetup>().OnActivate(_pool);
    }
    public void AddMap()
    {
        GameObject newMap = _pool.GetNewMap();
        Vector3 newPos = _movingMaps[_movingMaps.Length - 1].transform.position + _newZPos;
        _tempMap.GetComponent<MapSetup>().OnDeactivate();
        _tempMap.SetActive(false);

        _tempMap = _movingMaps[0];
        for (int i = 0; i < _movingMaps.Length - 1; i++)
            _movingMaps[i] = _movingMaps[i + 1];
        _movingMaps[_movingMaps.Length - 1] = newMap;

        newMap.transform.position = newPos;
        newMap.SetActive(true);
        newMap.GetComponent<MapSetup>().OnActivate(_pool);
    }
}
