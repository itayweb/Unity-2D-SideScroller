using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject player;

    private PlayerHealth playerHealth;

    public GameObject spawnPoint;

    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerHealth>();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
        if (playerHealth.playerCurrentHealth <= 0){
            Debug.Log("its working");
        }
    }

    void PlayerRespawn(){
        if(playerHealth.playerCurrentHealth <= 0){
            Debug.Log("Working");
        }
    }
    
    /*public void GameOver(){
        gameOver = true;
        gameOverMenu.SetActive(true);
    }

    public void RetryGame(){
        gameOver = false;
        gameOverMenu.SetActive(false);
        player.transform.position = spawnPoint.transform.position;
    }*/
}
