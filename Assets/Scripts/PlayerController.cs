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
	
	// taking damage
	public bool isAlive;
	public BoxCollider2D collider;
	public BoxCollider2D colliderOnDeath;
	public int health;
	public int lives;
	public GameObject GameOverScreen;
	
	public GameObject lifePanel;	// panel at the top showing lives
	public GameObject lifeImagePrefab;
	
	public PlayerAnimation animation;
	public ProjectileController projectileController;

    void Awake()
    {
		isAlive = true;
		setLifePanel(lives);
        rb = GetComponent<Rigidbody2D>();
		timeSinceLastFire = 0f;
    }

   

    void FixedUpdate()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
		if(isAlive){
			isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, groundObjects);
			ProcessInputs();
			Move();
		}
        Animate();
		
		timeSinceLastFire += Time.deltaTime;

    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping && !jumpDown)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;
    }
    private void Animate()
    {
		if(isAlive){
			if(Mathf.Abs(moveDirection) > .1f){
			
				if (moveDirection > 0 && !facingRight)
				{
					FlipCharacter();
				}
				else if (moveDirection < 0 && facingRight)
				{
					FlipCharacter();
				}
					
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
		else{
			animation.setAnimation(PlayerAnimation.DYING);
		}
    }
    private void ProcessInputs()
    {
		
		if(isAlive){
			
			moveDirection = Input.GetAxis("Horizontal");
		
			if(Input.GetKey(KeyCode.A)){
				moveDirection = -1f;
			}	
			if(Input.GetKey(KeyCode.D)){
				moveDirection = 1f;
			}

		
			if(Input.GetButton("Prone"))
			{
				isProning = true;
				if (Input.GetButtonDown("Jump"))
				{
					jumpDown = true;
				}
				jumpDown = false;
			}
        
			else if (Input.GetButtonDown("Jump") && isGrounded && !isProning)
			{
				isJumping = true;
			}	
			else
			{
				isProning = false;
			}
        
		
			if(Input.GetMouseButton(0)){
				if(timeSinceLastFire >= fireCooldown){
					fire();
					timeSinceLastFire = 0f;
				}
			}
		}
		
		
		
		// MOUSE AIMING
		/*
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
		*/

    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
		if(facingRight){
			transform.localScale = new Vector2(1f, 1f);
		}
		else{
			transform.localScale = new Vector2(-1f, 1f);
		}
    }
	
	private void fire(){
		
		// calculate direction of bullet
		float velX;
		float velY;
		if(facingRight){ velX = 1f; }
		else{ velX = -1f; }
		velY = 0f;
		Vector2 bulletVelocity = (new Vector2(velX, velY).normalized)*8f;
		
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = bulletOriginT.position;
		bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
		
		projectileController.addProjectile(bullet);
	}
	
	private void takeHit(){
		if(isAlive){
			health--;
			animation.takeHit();
			if(health <= 0){
				collider.enabled = false;
				colliderOnDeath.enabled = true;
				lives--;
				setLifePanel(lives);
				if(lives > 0){
					die(true);
				}
				else{
					die(false);
					gameOver();
				}
			}
		}
	}
	
	private void die(bool respawn){
		isAlive = false;
		animation.die();
		if(respawn){
			StartCoroutine(respawnAfterDeath());
		}
		else{
			StartCoroutine(destroyAfterDeath());
		}
	}
	IEnumerator respawnAfterDeath(){
		yield return new WaitUntil(() => animation.deathComplete);
		animation.deathComplete = false;
		isAlive = true;
		health = 1;
		rb.AddForce(Vector2.up*jumpForce*.7f);
		collider.enabled = true;
		colliderOnDeath.enabled = false;
	}
	IEnumerator destroyAfterDeath(){
		yield return new WaitUntil(() => animation.deathComplete);
		GameObject.Destroy(this.gameObject);
		GameOverScreen.SetActive(true);
	}
	
	private void gameOver(){
		
	}
	
	// sets number of icons shown at the top
	void setLifePanel(int lives){
		foreach (Transform child in lifePanel.transform) {
			GameObject.Destroy(child.gameObject);
		}
		for(int i = 0; i < lives; i++){
			Instantiate(lifeImagePrefab, lifePanel.transform);
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider){
		GameObject obj = otherCollider.gameObject;
		if(obj.tag == "Projectile"){
			takeHit();
			GameObject.Destroy(obj);
		}
        else if(obj.tag == "Powerup")
        {
            GameObject.Destroy(obj);
        }
	}

}
