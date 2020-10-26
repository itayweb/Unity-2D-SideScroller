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

    public float coolDownTime;
    private float nextAttackTime = 0;

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
    public bool isFlipped;

    private Transform target;

    public float enemyRange = 3;

    public LayerMask playDetection;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public int attackDamage;
    public float attackRange;

    private float distToPlayer;

    RaycastHit2D hit;
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
        //if (distToPlayer < 1.5)
        //{
        //    rb.velocity = new Vector2(0, 0);
        //    if (transform.position.x > target.position.x)
        //    {
        //        state = State.Left;
        //        if (Physics2D.Raycast(transform.position, Vector2.left, 1))
        //        {
        //            Attack();
        //        }
        //    }
        //    else
        //    {
        //        state = State.Right;
        //        if (Physics2D.Raycast(transform.position, Vector2.right, 1))
        //        {
        //            Attack();
        //        }
        //    }
        //    //if (Time.time >= nextAttackRate)
        //    //{
        //    //    Attack();
        //    //    nextAttackRate = Time.time + 1 / attackRate;
        //    //    Debug.Log(nextAttackRate);
        //    //}

        //}
    }

    private void FixedUpdate()
    {
        distToPlayer = Vector2.Distance(transform.position, target.position);

        if (distToPlayer < 1.5)
        {
            rb.velocity = new Vector2(0, 0);
            if (transform.position.x > target.position.x)
            {
                state = State.Left;
                if (Time.time > nextAttackRate && Physics2D.Raycast(transform.position, Vector2.left, 1))
                {
                    Attack();
                    nextAttackRate = Time.time + coolDownTime;
                }
            }
            else
            {
                state = State.Right;
                if (Time.time > nextAttackRate && Physics2D.Raycast(transform.position, Vector2.right, 1))
                {
                    Attack();
                    nextAttackRate = Time.time + coolDownTime;
                }
            }
        }
    }

    public void FlipDemon()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > target.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < target.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
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
            Debug.Log("Testing");
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
        if (state == State.Right)
        {
            anim.SetInteger("state", 4);
            playerObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        else if (state == State.Left)
        {
            anim.SetInteger("state", 3);
            playerObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
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