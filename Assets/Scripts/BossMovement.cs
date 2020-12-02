using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public ProjectileController projectileController;
    public Transform bulletOriginT;
    public Transform player;
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    [SerializeField] float fireCooldown;
    private float timeSinceLastFire;
    public Rigidbody2D rb;
    public bool facingRight;
    public Transform firepoint;
    public GameObject bulletPrefab;
   


    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
 
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;

        timeSinceLastFire += Time.deltaTime;

        
		if(timeSinceLastFire >= fireCooldown){
			fire();
			timeSinceLastFire = 0f;
		}

    }
    void fire(){
		    Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
	}
    // Update is called once per frame
    void FixedUpdate()
    {
		
    }
}




