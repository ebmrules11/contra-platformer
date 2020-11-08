using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform GroundCheck;
    public LayerMask groundObjects;
    public float checkRadius;

    private bool isJumping = false;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, groundObjects);



        Move();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        Animate();

       
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;
    }
    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
