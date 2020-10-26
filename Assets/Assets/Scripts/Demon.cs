using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public EnemyHealthBar enemyHealthBar;
    private Animator anim;
    public float health;
    public float attackDamage;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask playerDetection;
    public Transform target;
    private bool isFlipped;
    [SerializeField] GameObject trophy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyHealthBar.SetMaxEnemyHealth((int)health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D info = Physics2D.OverlapCircle(pos, attackRange, playerDetection);
        if (info != null)
        {
            info.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        enemyHealthBar.SetEnemyHealth((int)health);
        anim.SetTrigger("Hurt");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("Die");
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        trophy.SetActive(true);
        Destroy(this.gameObject);
    }
}
