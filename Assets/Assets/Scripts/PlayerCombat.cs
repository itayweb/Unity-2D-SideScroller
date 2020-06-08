using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 10;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemiesLayer;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("mouse 0") || Input.GetKeyDown("left ctrl")){
            Attack();
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
