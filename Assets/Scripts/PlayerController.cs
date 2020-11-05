using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D r2d;
    public float speedX;
    public float jumpForce;
    public bool isGrounded = false;
    public bool hittingWall = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor;
    float lastTimeGrounded;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }
    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if(collider != null)
        {
            isGrounded = true;
        }
        else
        {
            if(isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }

    }
    void Move()
    {
        if(!hittingWall)
        {
        r2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, r2d.velocity.y);

        }
    }

    void BetterJump()
    {
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (isGrounded ||
            Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpForce);
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 8 && !isGrounded)
        {

            hittingWall = true;
        }
        else
        {
            hittingWall = false;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        hittingWall = false;
    }
    // Update is called once per frame
    void Update()
    {

        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }
}
