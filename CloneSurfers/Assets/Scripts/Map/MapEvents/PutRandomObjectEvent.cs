using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutRandomObjectEvent : MonoBehaviour, IMapEvent
{
    [SerializeField] Transform[] _wallsInMap;
    [SerializeField] Transform[] _positions;
    bool[] _isTaken;

    private void Awake()
    {
        _isTaken = new bool[_positions.Length];
    }
    public void OnActivate()
    {
        for (int i = 0; i < _isTaken.Length; i++)
            _isTaken[i] = false;

    }

    public void OnDeactivate()
    {
        for (int i = 0; i < _wallsInMap.Length; i++)
        {
            int position;

            do position = Random.Range(0, _positions.Length);
            while (_isTaken[position]);

            _isTaken[position] = true;
            _wallsInMap[i].position = _positions[position].position;

        }
    }
}
