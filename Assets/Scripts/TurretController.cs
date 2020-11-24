using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public Transform player;
    public float fireCooldown;
    public float turretTimeStep;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = player.position - transform.position;
        float rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (currentTime > turretTimeStep)
        {
            rb.rotation = Mathf.Round(rawAngle / 45) * 45;
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
        timeSinceLastFire += Time.deltaTime;
    }
}
