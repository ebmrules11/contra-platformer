using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private LayerMask fallThroughPlatform;
    private GameObject contra;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        contra = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Prone"))
        {
            if (Input.GetButton("Jump"))
            {
                StartCoroutine(flip());
                //contra.layer = LayerMask.NameToLayer("IgnorePlatforms");
            }
            else
            {
                //contra.layer = LayerMask.NameToLayer("Player");
            }
        }
        else
        {
           // contra.layer = LayerMask.NameToLayer("Player");
        }
    }

    IEnumerator flip()
    {
        contra.layer = LayerMask.NameToLayer("IgnorePlatforms");
        yield return new WaitForSecondsRealtime(0.5f);
        contra.layer = LayerMask.NameToLayer("Player");
    }
}
