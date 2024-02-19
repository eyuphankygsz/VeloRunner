using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    float[] _lanePositions;
    [SerializeField] float _laneGap;
    int _lanes = 3;

    Vector3 _startPos;
    int _currentLane;

    [SerializeField] int _direction;
    bool _move;

    public PlayerController _controller;
    public Vector3 StartPosition { get; private set; }

    void Awake()
    {
        InitializeLanes();
    }
    void InitializeLanes()
    {
        _lanePositions = new float[_lanes];
        _lanePositions[0] = transform.position.x;

        for (int i = 1; i < _lanes; i++)
            _lanePositions[i] = _lanePositions[i - 1] + _laneGap;

        _currentLane = Mathf.CeilToInt(_lanes / 2);
        StartPosition = new Vector3(_lanePositions[_currentLane], transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted || !_move)
            return;


        if (transform.position.x != _lanePositions[_currentLane])
        {
            _controller.RB.velocity = new Vector3(_direction * _controller.Speed, _controller.RB.velocity.y, _controller.RB.velocity.z);
        }

        if ((_direction == -1 && transform.position.x < _lanePositions[_currentLane]) || 
            (_direction == 1 && transform.position.x > _lanePositions[_currentLane]))
        {
            _controller.RB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            _controller.RB.velocity = new Vector3(0, _controller.RB.velocity.y, _controller.RB.velocity.z);
            transform.position = new Vector3(_lanePositions[_currentLane], transform.position.y, transform.position.z);
            _move = false;
        }
    }
    public void MoveTo(bool right)
    {
        _controller.RB.constraints = RigidbodyConstraints.FreezeRotation;
        _startPos = transform.position;
        if ((right && _currentLane == _lanes - 1) || !right && _currentLane == 0)
            return;

        _direction = right ? 1 : -1;
        _currentLane += _direction;
        _move = true;
    }
}
