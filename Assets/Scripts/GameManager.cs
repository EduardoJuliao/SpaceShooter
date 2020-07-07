using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    public void GameOver()
    {
        _isGameOver = true;
    }

    private void Update()
    {
        if (_isGameOver && Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene("Scenes/Game");
        }
    }
}
