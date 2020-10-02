using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Left,
        Right,
        AttackLeft,
        AttackRight,
        PlayerDetectionLeft,
        PlayerDetectionRight,
        HurtLeft,
        HurtRight,
        DyingLeft,
        DyingRight,
    }

    private State state;

    private float attackRate = 10f;
    private float nextAttackRate = 0f;

    public int maxHealth = 1500;
    private int currentHealth;
    public int distance = 6;

    public EnemyHealthBar enemyHealthBar;
    public Canvas canvas;
    
    public GameObject playerObject;
    public GameObject trophy;

    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 4f;
    public float timerSeconds;

    public bool movingLeft;

    private Transform target;

    public float enemyRange = 3;

    public LayerMask playDetection;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public int attackDamage;
    public float attackRange;

    private float distToPlayer;
    //private float nextAttackTime = 0f;
    //public float attackRate = 5f;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Left;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyHealthBar.SetMaxEnemyHealth(maxHealth);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, target.position);

        if (distToPlayer < enemyRange && distToPlayer > 1.5)
        {
            ChasingPlayer();
        }
        else
        {
            StopChasingPlayer();
        }
        if (distToPlayer < 1.5)
        {
            rb.velocity = new Vector2(0, 0);
            if (transform.position.x > target.position.x)
            {
                state = State.Left;
            }
            else
            {
                state = State.Right;
            }
            Attack();
        }
    }

    void ChasingPlayer()
    {
        if (transform.position.x > target.position.x)
        {
            //Moving Demon right:
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            state = State.Left;
            anim.SetInteger("state", (int)state);
        }
        else
        {
            //Moving Demon left:
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            state = State.Right;
            anim.SetInteger("state", (int)state);
        }
        if (transform.position.x == target.position.x)
        {
            StopChasingPlayer();
        }
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
        state = State.Idle;
    }

    void Attack()
    {
        if (Time.time >= nextAttackRate)
        {
            if (state == State.Right)
            {
                anim.SetInteger("state", 4);
                playerObject.GetComponent<PlayerHealth>().TakeDamage();
            }
            else if (state == State.Left)
            {
                anim.SetInteger("state", 3);
                playerObject.GetComponent<PlayerHealth>().TakeDamage();
            }
            nextAttackRate = Time.time + 1 / attackRate;
            Debug.Log(nextAttackRate);
        }
    }

    public void TakeDamage(int playerAttackDamage)
    {
        currentHealth -= playerAttackDamage;
        enemyHealthBar.SetEnemyHealth(currentHealth);
        if (transform.position.x > target.position.x)
        {
            state = State.HurtLeft;
            anim.SetInteger("state", (int)state);
        }
        else
        {
            state = State.HurtRight;
            anim.SetInteger("state", (int)state);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dying");
        if (transform.position.x > target.position.x)
        {
            state = State.DyingLeft;
            anim.SetInteger("state", (int)state);
        }
        else
        {
            state = State.DyingRight;
            anim.SetInteger("state", (int)state);
        }
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Timer());
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(1);
            trophy.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}