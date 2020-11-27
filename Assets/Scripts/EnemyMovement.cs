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
	public GameObject player;
	public float sensingDistance;
	public bool playerDetected;
	
	// shooting
	[SerializeField] ProjectileController projectileController;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform bulletOriginT;
	[SerializeField] float fireCooldown;
	float timeSinceLastFire;
	
	// taking damage
	public bool isAlive;
	public BoxCollider2D collider;
	public BoxCollider2D colliderOnDeath;
	public int health;
	
    // Start is called before the first frame update
    void Start()
    {
		isAlive = true;
        facingRight = true;
    }
	
	void FixedUpdate(){
		
		if(isAlive){
			checkSurroundings();
			if(isGrounded){
				walk();
			}
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
		
		// detect if player is within range;
		float distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
		if(distanceFromPlayer < sensingDistance){
			playerDetected = true;
		}else{ playerDetected = false; }
		
		// if player detected, face player and fire
		if(playerDetected){
			if(timeSinceLastFire >= fireCooldown){
				fire();
				timeSinceLastFire = 0f;
			}
			
			// get direction between enemy and player
			Vector2 dir = player.transform.position - bulletOriginT.transform.position;
			
			// turn towards player's direction
			if(Mathf.Abs(dir.x) > 1f){
				facingRight = (dir.x >= 0f);
				if(facingRight){
					transform.localScale = new Vector2(1f, 1f);
				}
				else{
					transform.localScale = new Vector2(-1f, 1f);
				}
			}
			if(edgeDetected || wallDetected){
				moveSpeed = moveSpeed_stop;
			}else{ moveSpeed = moveSpeed_full; }
		}
		
		// else, do normal patrol
		else{
			if(edgeDetected || wallDetected){
				facingRight = !facingRight;
				if(facingRight){
					transform.localScale = new Vector2(1f, 1f);
				}
				else{
					transform.localScale = new Vector2(-1f, 1f);
				}
			}
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
		Vector2 dir = player.transform.position - bulletOriginT.transform.position;
		float velX;
		float velY;
		if(facingRight){ velX = 1f; }
		else{ velX = -1f; }
		if(Mathf.Abs(dir.y) >= (dir.magnitude)/2f){
			if(dir.y > 0f){
				velY = 1f;
			} else{ velY = -1f; }
		} else{ velY = 0f; }
		Vector2 bulletVelocity = (new Vector2(velX, velY).normalized)*8f;
		
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = bulletOriginT.position;
		bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
		
		projectileController.addProjectile(bullet);
	}
	
	void animate(){
		if(isAlive){
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
		else{
			animation.setAnimation(PlayerAnimation.DYING);
		}
	}
	
	
	private void takeHit(){
		health--;
		animation.takeHit();
		if(health <= 0){
			collider.enabled = false;
			colliderOnDeath.enabled = true;
			die();
		}
	}
	
	private void die(){
		isAlive = false;
		animation.die();
		StartCoroutine(destroyAfterDeath());
	}
	IEnumerator destroyAfterDeath(){
		yield return new WaitUntil(() => animation.deathComplete);
		GameObject.Destroy(this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider){
		GameObject obj = otherCollider.gameObject;
		if(obj.tag == "Projectile"){
			takeHit();
			GameObject.Destroy(obj);
		}
	}
}
