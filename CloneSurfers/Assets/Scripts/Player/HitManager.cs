using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField] LayerMask _obstacleLayerMask, _playerLayerMask;
    float _hitExitTimer, _defaultHitExitTime = 3f;
    bool _canCount;
    PlayerController _playerController;

    int _healthCount = 3;
    [SerializeField] TextMeshProUGUI healthText;
    string _healthPrefix = "Health\n";

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    public void StartHit()
    {
        if (_canCount)
            return;
        _healthCount -= 1;
        healthText.text = _healthPrefix + _healthCount;
        if (_healthCount == 0)
        {
            GameManager.Instance.GameOver();
            return;
        }
        _playerController._hitRoot.StartHit();
        TurnObstacleHit(true);
        _hitExitTimer = _defaultHitExitTime;
        _canCount = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canCount) return;
        _playerController._hitRoot.UpdateState();
        _hitExitTimer -= Time.deltaTime;
        if (_hitExitTimer <= 0)
        {
            _playerController._hitRoot.ReturnToNormal();
            _canCount = false;
            TurnObstacleHit(false);
        }

    }

    void TurnObstacleHit(bool turn)
    {
        Physics.IgnoreLayerCollision(7,8,turn);
    }
}
