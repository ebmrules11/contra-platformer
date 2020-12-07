using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	
	public GameObject objectToFollow;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(objectToFollow != null){
			transform.position = Vector3.Lerp(transform.position, objectToFollow.transform.position + Vector3.back*7.75f, 20f*Time.deltaTime);
		}
	}
}
