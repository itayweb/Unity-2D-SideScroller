﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;

    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}