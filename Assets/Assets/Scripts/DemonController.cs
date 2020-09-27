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
    }

    private State state;

    public int maxHealth = 1500;
    private int currentHealth;
    public int distance = 6;

    public EnemyHealthBar enemyHealthBar;
    public Canvas canvas;
    
    public GameObject coin;
    public GameObject healthPoint;

    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 4f;

    public bool movingLeft;

    private Transform target;

    public float enemyRange = 3;

    public LayerMask playDetection;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public int attackDamage;
    public float attackRange;

    private float distToPlayer;
    private float nextAttackTime = 0f;
    public float attackRate = 5f;

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
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return new WaitForSeconds(1);
                Attack();
            }
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

    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
        state = State.Idle;
    }

    void Attack()
    {
        if (state == State.Right)
        {
            anim.SetInteger("state", 4);
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, playDetection);
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<PlayerHealth>().TakeDamage();
            }
        }
        else if (state == State.Left)
        {
            anim.SetInteger("state", 3);
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, playDetection);
            foreach(Collider2D player in hitPlayer)
            {
                player.GetComponent<PlayerHealth>().TakeDamage();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);

        if (attackPointLeft == null) { return; }

        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }

    public void TakeDamage(int playerAttackDamage)
    {
        Debug.Log("Working");
        currentHealth -= playerAttackDamage;
        enemyHealthBar.SetEnemyHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dying");
    }
}