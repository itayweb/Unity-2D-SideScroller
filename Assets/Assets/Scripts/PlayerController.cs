using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    public Transform groundCheck;
    public LayerMask groundObjects;

    public float movementSpeed;
    private float moveDirection;
    public float jumpForce;
    public float checkRadius;

    private Rigidbody2D rb;
    
    private bool facingRight = true;
    private bool isJumping = false;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));

        // Getting the player inputs:
        Inputs();

        // Fliping the rotation of the player character, right and left:
        FlipingCharacter();
        
    }
    
    private void FixedUpdate(){
        Move();

        // Ground check:
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
    }

    private void Inputs(){
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded){
            Debug.Log("jump");
            isJumping = true;
            anim.SetBool("IsJumping", true);
        }
    }

    void OnCollisionEnter2D(Collision2D player){
        if (player.collider.tag == "Ground"){
            anim.SetBool("IsJumping", false);
        }
    }

    private void Move(){
        rb.velocity = new Vector2(moveDirection * movementSpeed, rb.velocity.y);
        if(isJumping){
            rb.velocity = new Vector2(0f,jumpForce);
        }
        isJumping = false;
    }

    private void FlipingCharacter(){
        if(moveDirection > 0 && !facingRight){
            Flip();
        }
        else if(moveDirection < 0 && facingRight){
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
}
