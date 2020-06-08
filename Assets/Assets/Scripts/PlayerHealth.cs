using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public LayerMask playerLayer;

    public float hitRange = 0.5f;

    private Collider2D col;

    public int enemyAttackDamage = 5;
    public int playerMaxHealth = 60;
    public int playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        playerCurrentHealth = playerMaxHealth;
    }
    
    void PlayerTakeDamage(){
        
        Collider2D[] enemyHitPlayer = Physics2D.OverlapCircleAll(gameObject.transform.position.x,hitRange);
    }

    /*void OnCollisionEnter2D(Collision2D col){
        //if (col.gameObject.tag == "Enemy"){
            playerCurrentHealth -= enemyAttackDamage;

            if(playerCurrentHealth <= 0){
                playerDie();
            }
        //}
    }*/

    void playerDie(){
        Debug.Log("Player died");
    }
}
