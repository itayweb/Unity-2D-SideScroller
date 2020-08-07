using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public LayerMask playerDetection;

    private Transform leftDirection;
    private Transform rightDirection;

    public GameObject coin;
    public GameObject healthPoint;

    public EnemyHealthBar enemyHealthBar;
    public Canvas canvas;

    public float moveSpeed = 4f;
    public bool movingLeft;

    private Animator anim;
    private Rigidbody2D rb;

    public int maxHealth = 50;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar.SetMaxEnemyHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update() {
        if (movingLeft == true){
            rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y);
        }
        else if (movingLeft == false){
            rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "EnemyWalkPointLeft"){
            transform.Rotate(0f,180f,0f);
            canvas.transform.Rotate(0f,180f,0f);
            movingLeft = false;
        }
        if(collider.gameObject.tag == "EnemyWalkPointRight"){
            transform.Rotate(0f,180f,0f);
            canvas.transform.Rotate(0f,180f,0f);
            movingLeft = true;
        }
    }

    void Attack(){
    }
    
    public void TakeDamage(int playerAttackDamage){
        // Substracting the current health with the attack damage:
        currentHealth -= playerAttackDamage;

        moveSpeed = 0f;
        StartCoroutine(Timer());
        IEnumerator Timer(){
            yield return new WaitForSeconds(1);
            moveSpeed = 4f;
        }

        // Playing enemy hurt animation:
        anim.SetTrigger("Hurt");

        enemyHealthBar.SetEnemyHealth(currentHealth);
        
        // Killing the enemy and die:
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy died");

        // Playing die animation:
        anim.SetTrigger("Death");

        GameObject a = Instantiate(healthPoint) as GameObject;
        a.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        GameObject b = Instantiate(coin) as GameObject;
        b.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1f);
        
        // Disable the enemy:
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        canvas.enabled = false;
        StartCoroutine(EnemyDeathTimer());
        IEnumerator EnemyDeathTimer(){
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}