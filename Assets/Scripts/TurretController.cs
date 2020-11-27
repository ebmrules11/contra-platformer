using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public Transform player;
	
	public ProjectileController projectileController;
	public GameObject bulletPrefab;
	public Transform bulletOriginT;
    public float fireCooldown;
    public float turretTimeStep;
	public float sensingDistance;

    private Rigidbody2D rb;
    private float currentTime = 0f;
    private float timeSinceLastFire = 0f;


    float degToRad(float degrees)
    {
        return degrees * Mathf.Deg2Rad;
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
	
	void fire(){
		// calculate direction of bullet
		Vector2 bulletVelocity = transform.TransformDirection(Vector3.right)*8f;
		
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = bulletOriginT.position;
		bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
		
		projectileController.addProjectile(bullet);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		float distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
		
		// rotate to player and fire if within sensing range
		if(distanceFromPlayer < sensingDistance){
			Vector3 dir = player.position - transform.position;
			float rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if (currentTime > turretTimeStep)
			{
				rb.rotation = Mathf.Round(rawAngle / 45) * 45;
				currentTime = 0;
			}
			currentTime += Time.deltaTime;
		
			if(timeSinceLastFire >= fireCooldown){
				fire();
				timeSinceLastFire = 0f;
			}
		}
		timeSinceLastFire += Time.deltaTime;
    }
}
