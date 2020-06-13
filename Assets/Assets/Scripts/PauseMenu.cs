using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseScreen;
    public GameObject pauseBtn;

    public void PauseGame(){
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        pauseBtn.SetActive(false);
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        pauseBtn.SetActive(true);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}