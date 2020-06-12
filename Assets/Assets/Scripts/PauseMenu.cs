using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}