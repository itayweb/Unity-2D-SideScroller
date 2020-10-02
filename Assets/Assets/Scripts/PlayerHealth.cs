using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints = 10;
    public int deathDelay = 0;
    public int timeDelay = 10;

    public GameObject gameOverScreen;
    
    public HealthBar healthBar;

    private Animator animator;

    public float hitRange = 0.5f;
    public float enemyAttackDamage = 0.1f;

    public int playerMaxHealth = 60;
    public float playerCurrentHealth;
    
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);   
    }

    void OnCollisionEnter2D(Collision2D collision){
        /*if(collision.gameObject.CompareTag("Enemy")){
            TakeDamage();
        }*/
        if(collision.gameObject.CompareTag("Obsicle")){
            playerDie();
        }
        /*if(collision.gameObject.CompareTag("HealthPoints")){
            HealthPoints();
        }*/
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag.Equals("Enemy")){
            TakeDamage();
        }
        if (collider.gameObject.tag.Equals("HealthPoints")){
            HealthPoints();
        }
    }

    void HealthPoints(){
        playerCurrentHealth += healthPoints;
        Destroy(GameObject.FindWithTag("HealthPoints"));
    }

    public void TakeDamage(){
        playerCurrentHealth -= enemyAttackDamage;
        healthBar.SetHealth((int)playerCurrentHealth);
        animator.SetTrigger("Hurt");
        if (playerCurrentHealth <= 0)
        {
            Debug.Log(animator.GetBool("IsDead"));
            playerDie();
        }
    }

    public void playerDie(){
        animator.SetBool("IsDead",true);
        StartCoroutine (DeathTimer());
        IEnumerator DeathTimer(){
            yield return new WaitForSeconds(1);
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        }
    }
}
