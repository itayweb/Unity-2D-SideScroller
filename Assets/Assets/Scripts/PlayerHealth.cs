using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;

    private Animator animator;

    public float hitRange = 0.5f;

    public int enemyAttackDamage = 5;
    public int playerMaxHealth = 60;
    public int playerCurrentHealth;

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

    void TakeDamage(){
        playerCurrentHealth -= enemyAttackDamage;
        animator.SetTrigger("Hurt");
        healthBar.SetHealth(playerCurrentHealth);
        if (playerCurrentHealth <= 0){
            playerDie();
        }
    }

    void playerDie(){
        animator.SetBool("IsDead",true);
    }
}
