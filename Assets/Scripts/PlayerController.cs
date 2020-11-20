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
    public bool isProning = false;
    public bool jumpDown = false;
    public Rigidbody2D rb;
    public bool facingRight = true;
    private float moveDirection;
    [SerializeField] private bool isGrounded;
	
	public Vector2 mousePos;
	private bool lookingUp;
	private bool lookingDown;
	
	// shooting
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform bulletOriginT;
	[SerializeField] float fireCooldown;
	float timeSinceLastFire;
	
	public PlayerAnimation animation;
	public ProjectileController projectileController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
		
		timeSinceLastFire = 0f;
    }

   

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, groundObjects);
		
		timeSinceLastFire += Time.deltaTime;
		

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
        if (isJumping && !jumpDown)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
       /*else if (jumpDown)
        {
            effector.rotationalOffset = 180f;
        }*/
        isJumping = false;
    }
    private void Animate()
    {
	
		if(Mathf.Abs(moveDirection) > .1f){
			/*
			if (moveDirection > 0 && !facingRight)
			{
				FlipCharacter();
			}
			else if (moveDirection < 0 && facingRight)
			{
				FlipCharacter();
			}
			*/
			if(isGrounded){
				animation.setAnimation(PlayerAnimation.RUNNING);
			}
			else{
				animation.setAnimation(PlayerAnimation.JUMPING);
			}
		}
		else{
			if(isGrounded){
				animation.setAnimation(PlayerAnimation.STANDING);
			}
			else{
				animation.setAnimation(PlayerAnimation.JUMPING);
			}
		}
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
		
		// calculate position of mouse cursor
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float dx = mousePos.x - transform.position.x;
		float dy = mousePos.y - transform.position.y;
		if(dx > 0f){
			if(!facingRight){
				FlipCharacter();
			}
		}else{
			if(facingRight){
				FlipCharacter();
			}
		}
		if(Mathf.Abs(dy) > 3f){
			if(dy > 0f){
				lookingUp = true;
			}else{
				lookingDown = true;
			}
		}else{
			lookingUp = false;
			lookingDown = false;
		}
		
        
        if(Input.GetButton("Prone"))
        {
            isProning = true;
            if (Input.GetButtonDown("Jump"))
            {
                jumpDown = true;
            }
            StartCoroutine(SetjumpDownFalse());
        }
        
        else if (Input.GetButtonDown("Jump") && isGrounded && !isProning)
        {
            isJumping = true;
        }
        isProning = false;
		
		if(Input.GetMouseButton(0)){
			if(timeSinceLastFire >= fireCooldown){
				fire();
				timeSinceLastFire = 0f;
			}
		}

    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        //transform.Translate(0f, 180f, 0f, Space relativeTo = Space.Self);
    }

    IEnumerator SetjumpDownFalse()
    {
        yield return new WaitForSeconds(.75f);
        jumpDown = false;
    }
	
	
	private void fire(){
		
		// calculate direction of bullet
		float velX;
		float velY;
		if(facingRight){ velX = 1f; }
		else{ velX = -1f; }
		
		
		
		//Vector2 bulletVelocity = (new Vector2(velX, velY).normalized)*8f;
		Vector2 bulletVelocity = ((mousePos - new Vector2(transform.position.x, transform.position.y)).normalized)*8f;
		
		
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = bulletOriginT.position;
		bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
		
		projectileController.addProjectile(bullet);
	}
}
