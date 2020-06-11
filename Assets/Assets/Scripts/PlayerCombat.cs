using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 10;

    public Transform attackPoint;

    public float attackRange = 0.5f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public LayerMask enemiesLayer;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime){   
            if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown("left ctrl")){
                Attack();
                nextAttackTime = Time.time + 1/attackRate;
            }
        }
    }

    void Attack(){
        // Playing the attack animation:
        anim.SetTrigger("Attack");

        // Detecting the enemies:
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,enemiesLayer);
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    
    private void OnDrawGizmosSelected() {
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
