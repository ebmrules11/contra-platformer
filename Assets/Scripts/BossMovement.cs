using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
	public float sensingDistance = 2f;

    public ProjectileController projectileController;
    public Transform bulletOriginT;
    public Transform player;

    public GameObject playerOBJ;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    [SerializeField] float fireCooldown;
    private float timeSinceLastFire;
    public Rigidbody2D rb;
    public bool facingRight;
    public Transform firepoint;
    public GameObject bulletPrefab;
    public AudioSource boss_audio;
    public AudioSource game_audio;
    public AudioSource boss_dead_audio;
    public AudioSource boss_after_dead_audio;
    private bool audioChanged = false;

    public float health = 150f;
    public PlayerAnimation animation;

    public bool isAlive;
	public BoxCollider2D collider;
	public BoxCollider2D colliderOnDeath;
   


    void Start()
    {
        game_audio.Pause();
        boss_audio.Play();
        
        
        isAlive = true;
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
 

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;

        float distanceFromPlayer = Vector2.Distance(transform.position, playerOBJ.transform.position);

       if(distanceFromPlayer < sensingDistance){
			if(health < 150 && hashit == false)
				fireCooldown - 0.2;
				hashit = true;
			if(health < 100 && hashit1 == false)
				fireCooldown - 0.2;
				hashit1 = true;
			if(health < 100 && hashit2 == false)
				fireCooldown - 0.2;
				hashit2 == true;
			if(timeSinceLastFire >= fireCooldown){
				fire();
				timeSinceLastFire = 0f;
			}
		}
		timeSinceLastFire += Time.deltaTime;
		

    }
    void fire(){



		Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
	}

    private void takeHit(){
		health--;
		//animation.takeHit();
		if(health <= 0){
			collider.enabled = false;
			colliderOnDeath.enabled = true;
			die();
		}
	}
    void OnTriggerEnter2D(Collider2D otherCollider){
		GameObject obj = otherCollider.gameObject;
		if(obj.tag == "PlayerProjectile"){
			takeHit();
			GameObject.Destroy(obj);
		}
	}

    private void die(){
		isAlive = false;
		animation.die();
        boss_audio.Pause();
        boss_dead_audio.Play();
		StartCoroutine(destroyAfterDeath());
        
	}
    
    IEnumerator destroyAfterDeath(){
		yield return new WaitUntil(() => animation.deathComplete);
        
        boss_after_dead_audio.Play();
		GameObject.Destroy(this.gameObject);
        
        
	}


    // Update is called once per frame
    void FixedUpdate()
    {
		
    }
}


