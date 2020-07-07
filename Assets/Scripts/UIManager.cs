using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartText;
    [SerializeField] private Image _livesDisplay;
    [SerializeField] private Sprite[] _liveSprites;

    private GameManager _gameManager;

    private void Start()
    {
        UpdateScoreText(0);
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null.");
        }
    }

    public void UpdateScoreText(long score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int lives)
    {
        _livesDisplay.sprite = _liveSprites[lives];
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
