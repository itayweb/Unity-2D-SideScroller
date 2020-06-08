using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;

    public int maxHealth = 50;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int playerAttackDamage){
        // Substracting the current health with the attack damage:
        currentHealth -= playerAttackDamage;

        // Playing enemy hurt animation:
        anim.SetTrigger("Hurt");
        
        // Killing the enemy and die:
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy died");

        // Playing die animation:
        anim.SetBool("IsDead", true);

        // Disable the enemy:
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}