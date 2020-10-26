using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private bool isHurting = false;

    public float coolDownTime;
    private float nextHurtTime = 0;

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
            print("testing");
            playerDie();
        }
        /*if(collision.gameObject.CompareTag("HealthPoints")){
            HealthPoints();
        }*/
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag.Equals("Enemy")){
            TakeDamage(enemyAttackDamage);
        }
        if (collider.gameObject.tag.Equals("HealthPoints")){
            if (playerCurrentHealth < playerMaxHealth)            
                HealthPoints();
        }
        else if (collider.gameObject.tag == "Obsicle")
        {
            playerDie();
        }
    }

    void HealthPoints(){
        playerCurrentHealth += healthPoints;
        healthBar.SetHealth((int)playerCurrentHealth);
        Destroy(GameObject.FindWithTag("HealthPoints"));
    }

    public void TakeDamage(float enemyAttackDamage){
        playerCurrentHealth -= enemyAttackDamage;
        healthBar.SetHealth((int)playerCurrentHealth);
        if (isHurting == false)
        {
            StartCoroutine(PlayerTakeDamageAnimation());
        }
        if (playerCurrentHealth <= 0)
        {
            animator.ResetTrigger("Attack");
            isHurting = true;
            playerDie();
        }
    }

    public void playerDie(){
        animator.SetTrigger("Die");
        StartCoroutine (DeathTimer());        
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(1.4f);
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    IEnumerator PlayerTakeDamageAnimation()
    {
        isHurting = true;
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Hurt");
        isHurting = false;
    }
}
