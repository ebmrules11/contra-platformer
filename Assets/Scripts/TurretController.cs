using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{


    public Transform player;


    private Rigidbody2D rb;
    //private float currentTime = 0f;

   
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;

        /*
        if (currentTime >= rotationWait)
        {
            // rotate image here

            currentTime = 0f;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        */
    }
}
