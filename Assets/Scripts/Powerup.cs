using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float maxSpeed = .1f;
    public float amplitude = .1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + amplitude * Mathf.Sin(Time.time * maxSpeed), 0);
    }
}
