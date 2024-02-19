using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] MapManager _mapManager;
    public MapManager MapManager { get { return _mapManager; } }

    public bool IsGameStarted;

    long _score = 0;
    float _timer = 0;
    int _highScore;

    public float _scoreMultiplier { get; private set; }

    //For Slowly Increase!
    float _scoreMultiplierIncreaseAmount = 15;
    float _maxMultiplier = 4.5f;

    [SerializeField] TextMeshProUGUI _scoreTxt;
    string _scorePrefix = "Score:\n";



    [SerializeField] GameObject _deathPanel;
    private void Awake()
    {
        _scoreMultiplier = 1;

        if (Instance == null)
        {
            Instance = this;
        }

    }


    public void AddScore(int score)
    {
        _score += (int)Mathf.Ceil(score * _scoreMultiplier);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        _deathPanel.SetActive(true);
    }
    public void ExitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime * _scoreMultiplier;
        if (_timer >= 1)
        {
            _timer = 0;
            _score += 1;
        }
        _scoreTxt.text = _scorePrefix + _score.ToString("D6");

        if (_scoreMultiplier < _maxMultiplier)
            _scoreMultiplier += Time.deltaTime / _scoreMultiplierIncreaseAmount;
    }
}
