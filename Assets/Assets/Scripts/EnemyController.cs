using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform enemyCheck;
    public Transform playerCheck;

    public Transform player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Mathf.Abs(enemyCheck.position.x) == Mathf.Abs(playerCheck.position.x)){
            Debug.Log("Touched");
            Destroy(this.gameObject);
        }
    }
}
