using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseScreen;
    public GameObject pauseBtn;

    public bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePaused){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
    }

    public void PauseGame(){
        gamePaused = true;
        pauseScreen.SetActive(true);
        pauseBtn.SetActive(false);
    }

    public void ResumeGame(){
        gamePaused = false;
        pauseScreen.SetActive(false);
        pauseBtn.SetActive(true);
    }
}
