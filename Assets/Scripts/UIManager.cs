using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Image _livesDisplay;
    [SerializeField] private Sprite[] _liveSprites;

    private void Start()
    {
        UpdateScoreText(0);
    }

    public void UpdateScoreText(long score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int lives)
    {
        _livesDisplay.sprite = _liveSprites[lives];
    }
}
