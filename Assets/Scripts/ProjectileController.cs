using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	
	public Stack<GameObject> bulletStack;
	public Stack<float> timeStack;
	
	[SerializeField] float bulletLifetime;
	
	
	
	public void addProjectile(GameObject proj){
		bulletStack.Push(proj);
		timeStack.Push(Time.time);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        bulletStack = new Stack<GameObject>();
        timeStack = new Stack<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeStack.Count > 0){
			float f = timeStack.Peek();
			if(Time.time - f > bulletLifetime){
				timeStack.Pop();
				GameObject bullet = bulletStack.Pop();
				GameObject.Destroy(bullet);
			}
		}
    }
}
