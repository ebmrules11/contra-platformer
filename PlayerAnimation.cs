<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	
   	public SpriteRenderer spriteRenderer;
	
	// arrays of sprites to cycle through
	public Sprite[] standing;
	public Sprite[] jumping;
	public Sprite[] running;
	
	
	
	int currentAction;
	Sprite[] spriteArray;
	int animIndex;
	float timePass;
	
	// 
	public static int STANDING = 0;
	public static int JUMPING = 1;
	public static int RUNNING = 2;
	
	
	
    // Start is called before the first frame update
    void Start()
    {
		spriteArray = standing;
		timePass = 0;
    }

    // Update is called once per frame
    void Update()
    {
	
		// cycle through the currently selected sprite array to animate character (based on time passed)
		if(timePass >= .1f){
			spriteRenderer.sprite = spriteArray[animIndex];
			animIndex++;
			if(animIndex >= spriteArray.Length){ animIndex = 0; }
			timePass = 0;
		}
		
		timePass += Time.deltaTime;
    }
	
	
	// sets array of sprites to cycle through
	public void setAnimation(int action){
		if(action != currentAction){
			currentAction = action;
			animIndex = 0;
			switch (action) {
				case 0:
					spriteArray = standing;
					break;
				case 1:
					spriteArray = jumping;
					break;
				case 2:
					spriteArray = running;
					break;
				default:
					break;
			}
		}
	}
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	
   	public SpriteRenderer spriteRenderer;
	
	public Sprite[] standing;
	public Sprite[] jumping;
	public Sprite[] running;
	public Sprite[] prone;
	
	
	
	int currentAction;
	Sprite[] spriteArray;
	int animIndex;
	float timePass;
	
	// 
	public static int STANDING = 0;
	public static int JUMPING = 1;
	public static int RUNNING = 2;
	public static int PRONE = 3;
	
	
	
    // Start is called before the first frame update
    void Start()
    {
		spriteArray = standing;
		setAnimation(STANDING);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
			setAnimation(JUMPING);
		}
		if(Input.GetKey(KeyCode.S)){
			setAnimation(STANDING);
		}
		if(Input.GetKey(KeyCode.D)){
			setAnimation(RUNNING);
		}
		
		if(timePass >= .1f){
			spriteRenderer.sprite = spriteArray[animIndex];
			animIndex++;
			if(animIndex >= spriteArray.Length){ animIndex = 0; }
			timePass = 0;
		}
		
		timePass += Time.deltaTime;
    }
	
	public void setAnimation(int action){
		if(action != currentAction){
			currentAction = action;
			animIndex = 0;
			switch (action) {
				case 0:
					spriteArray = standing;
					break;
				case 1:
					spriteArray = jumping;
					break;
				case 2:
					spriteArray = running;
					break;
				case 3:
					//spriteArray = shooting;
					break;
				default:
					break;
			}
		}
	}
}
>>>>>>> b4e7f43589ebaa9517935f9aedf507df0fcb315b
