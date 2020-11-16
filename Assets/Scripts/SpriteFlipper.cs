using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        gameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<PlayerController>().facingRight)
        {
            mySpriteRenderer.flipX = true;
        }
        if (gameObject.GetComponent<PlayerController>().facingRight)
        {
            mySpriteRenderer.flipX = false;
        }
    }
}
