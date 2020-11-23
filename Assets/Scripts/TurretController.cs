using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{


    public Transform player;
    public int[] angles;

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
        float rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        int angIdx = 0;

        

        rb.rotation = rawAngle;
    }
}
