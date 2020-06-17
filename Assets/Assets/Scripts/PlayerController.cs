using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int coins = 0;

    public Canvas canvasBoard; // Getting the health bar ui
    public Text numPoints;

    private Animator anim; // Getting the animator component

    public Transform groundCheck; // Getting the ground current coords
    public LayerMask groundObjects; // Setting what layers is known as "Ground"

    public float movementSpeed; // Setting the player's movement speed
    public float moveDirection; // Getting the player's movement direction
    public float jumpForce; // Setting the player's jump speed
    public float checkRadius; // Setting the radius between the ground and the player

    private Rigidbody2D rb; // Getting the rigidbody component
    
    private bool facingRight = true; // Setting the default player's condition direction (facing)
    private bool isJumping = false; // Setting the default player's condition if he is jumping
    private bool isGrounded; // Setting the player's condition if he is on the ground

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enable walk animation:
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));

        // Getting the player inputs funtion:
        Inputs();

        // Fliping the rotation of the player character, right and left, function:
        FlipingCharacter();
        
    }
    
    private void FixedUpdate(){
        // Calling the move function to move the player:
        Move();

        // Ground check:
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
    }

    public void Inputs(){
        // Getting the player's input axis:
        moveDirection = Input.GetAxis("Horizontal");
        // Checking if the player hit the jump button and if the condition (he is on the ground) are happening:
        if (Input.GetButtonDown("Jump") && isGrounded){
            isJumping = true; // Changing the player's condition if he is jumping
            anim.SetBool("IsJumping", true); // Enable the jump animation
        }
    }

    void OnCollisionEnter2D(Collision2D player){
        // Checking if the player is touching layers named as "Ground":
        if (player.collider.tag == "Ground"){
            anim.SetBool("IsJumping", false); // Disable the jump animation
        }
        // Checking if the player is touching the coin and adding it to the collector:
        if (player.gameObject.tag == "Coin"){
            Destroy(player.gameObject); // Remove the the coin from the screen
            coins += 1; // Adding 1 coin to the collector
            numPoints.text = coins.ToString();
        }
    }

    private void Move(){
        // Changing the player's direction (left or right) depend on the input he entered:
        rb.velocity = new Vector2(moveDirection * movementSpeed, rb.velocity.y);
        // Checking if the condition (if he is jumping) in happening:
        if(isJumping){
            rb.velocity = new Vector2(0f,jumpForce); // Changing the player's direction upwards
        }
        isJumping = false; // Changing the jumping condition to eliminate the option to jump infinite
    }
    // Fliping the character sprite function:
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
        canvasBoard.transform.Rotate(0f,180f,0f);
    }
}
