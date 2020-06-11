using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject gameOverScreen;
    
    public HealthBar healthBar;

    private Animator animator;

    public float hitRange = 0.5f;

    public int enemyAttackDamage = 5;
    public int playerMaxHealth = 60;
    public int playerCurrentHealth;
    
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            TakeDamage();
        }
    }

    public void TakeDamage(){
        playerCurrentHealth -= enemyAttackDamage;
        animator.SetTrigger("Hurt");
        healthBar.SetHealth(playerCurrentHealth);
        if (playerCurrentHealth <= 0){
            playerDie();
        }
    }

    public void playerDie(){
        animator.SetBool("IsDead",true);
    }
}
