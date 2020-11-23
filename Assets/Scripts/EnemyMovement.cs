using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	
	public Rigidbody2D rb;
	public bool facingRight;
	public float moveSpeed_full;
	public float moveSpeed_stop;
	public float moveSpeed;
	
	public PlayerAnimation animation;
	
	// edge and ground detection
	public Transform EdgeCheck;
	public Transform GroundCheck;
	public Transform WallCheck;
	public float checkRadius;
	public RaycastHit2D raycastHit;
	public LayerMask groundObjects;
	public bool edgeDetected;
	public bool wallDetected;
	public bool isGrounded;
	
	// player detection
	public bool playerDetected;
	public LayerMask playerMask;
	
	// shooting
	[SerializeField] ProjectileController projectileController;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform bulletOriginT;
	[SerializeField] float fireCooldown;
	float timeSinceLastFire;
	
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
    }
	
	void FixedUpdate(){
		
		checkSurroundings();
		
		if(isGrounded){
			walk();
		}
		animate();
		
	}

    // Update is called once per frame
    void Update()
    {
		
		timeSinceLastFire += Time.deltaTime;
        
    }
	
	void checkSurroundings(){
		
		// detect if on edge of a platform and if facing a wall
		edgeDetected = !Physics2D.OverlapCircle(EdgeCheck.position, checkRadius, groundObjects);
		wallDetected = Physics2D.OverlapCircle(WallCheck.position, checkRadius, groundObjects);
		
		// detect if grounded
		isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, groundObjects);
		
		// detect if player is in line of sight
		Vector2 lookDirection = Vector2.zero;
		if(facingRight){
			lookDirection = Vector2.right;
		}
		else{
			lookDirection = Vector2.left;
		}
		playerDetected = Physics2D.Raycast(transform.position, lookDirection, 10f, playerMask);
		
		
		if(edgeDetected || wallDetected){
			if(!playerDetected){
				facingRight = !facingRight;
				if(facingRight){
					transform.localScale = new Vector2(1f, 1f);
				}
				else{
					transform.localScale = new Vector2(-1f, 1f);
				}
			}
		}
		
		
		if(playerDetected){
			moveSpeed = moveSpeed_stop;
			if(timeSinceLastFire >= fireCooldown){
				fire();
				timeSinceLastFire = 0f;
			}
		}
		else{
			moveSpeed = moveSpeed_full;
		}
	}
	
	void walk(){
		Vector2 moveVector = Vector3.zero;
		if(facingRight){
			moveVector = Vector3.right*moveSpeed;
		}else{
			moveVector = Vector3.left*moveSpeed;
		}
		rb.velocity = moveVector;
	}
	
	void fire(){
		
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
	
	void animate(){
		if(moveSpeed > .1f){
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
}
