using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    //public GameObject gameObject;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        //gameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Prone"))
        {
            if (Input.GetButton("Jump"))
            {
                effector.rotationalOffset = 180f;
            }
            else
            {
                effector.rotationalOffset = 0;
            }
        }
        else
        {
            effector.rotationalOffset = 0;
        }
    }
}
