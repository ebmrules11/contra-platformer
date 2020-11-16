using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	
	public List<GameObject> bulletList;
	public List<float> timeList;
	
	[SerializeField] float bulletLifetime;
	
	
	
	public void addProjectile(GameObject proj){
		bulletList.Add(proj);
		timeList.Add(Time.time);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<GameObject>();
        timeList = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeList.Count > 0){
			float f = timeList[0];
			if(Time.time - f > bulletLifetime){
				GameObject bullet = bulletList[0];
				timeList.RemoveAt(0);
				bulletList.RemoveAt(0);
				GameObject.Destroy(bullet);
			}
		}
    }
}
