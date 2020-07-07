using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Image _livesDisplay;
    [SerializeField] private Sprite[] _liveSprites;

    private void Start()
    {
        UpdateScoreText(0);
        _gameOverText.gameObject.SetActive(false);
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
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
