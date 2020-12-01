using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	
   	public SpriteRenderer spriteRenderer;
	
	public Sprite[] standing;
	public Sprite[] jumping;
	public Sprite[] running;
	public Sprite[] prone;
	public Sprite[] dying;
	
	
	
	int currentAction;
	Sprite[] spriteArray;
	int animIndex;
	float timePass;
	
	// 
	public static int STANDING = 0;
	public static int JUMPING = 1;
	public static int RUNNING = 2;
	public static int PRONE = 3;
	public static int DYING = 4;
	
	bool changingSpriteColor;
	bool flashingSpriteColor;
	public bool deathComplete;
	
	
	
    // Start is called before the first frame update
    void Start()
    {
		spriteArray = standing;
		setAnimation(STANDING);
    }

    // Update is called once per frame
    void Update()
    {
		
		/*
        if(Input.GetKey(KeyCode.W)){
			setAnimation(JUMPING);
		}
		if(Input.GetKey(KeyCode.S)){
			setAnimation(STANDING);
		}
		if(Input.GetKey(KeyCode.D)){
			setAnimation(RUNNING);
		}
		*/
		
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
					spriteArray = prone;
					break;
				case 4:
					spriteArray = dying;
					break;
				default:
					break;
			}
		}
	}
	
	public void takeHit(){
		if(!changingSpriteColor){
			Color originalColor = spriteRenderer.color;
			Color hitColor = Color.red;
			StartCoroutine(changeSpriteColor(originalColor, hitColor, .2f));
		}
	}
	
	public void die(){
		if(!flashingSpriteColor){
			Color originalColor = spriteRenderer.color;
			Color flashColor = Color.clear;
			StartCoroutine(flashDeathColors(originalColor, flashColor, .1f));
		}
	}
	
	// starts another thread, allowing us to change color over time
	IEnumerator changeSpriteColor(Color c0, Color c1, float time){
		changingSpriteColor = true;
		spriteRenderer.color = c1;
		yield return new WaitForSeconds(time);
		spriteRenderer.color = c0;
		changingSpriteColor = false;
	}
	IEnumerator flashDeathColors(Color c0, Color c1, float flashSpeed){
		flashingSpriteColor = true;
		for(int i = 0; i < 10; i++){
			spriteRenderer.color = c1;
			yield return new WaitForSeconds(flashSpeed);
			spriteRenderer.color = c0;
			yield return new WaitForSeconds(flashSpeed);
		}
		deathComplete = true;
		for(int i = 0; i < 4; i++){
			spriteRenderer.color = c1;
			yield return new WaitForSeconds(flashSpeed);
			spriteRenderer.color = c0;
			yield return new WaitForSeconds(flashSpeed);
		}
		flashingSpriteColor = false;
		spriteRenderer.color = Color.white;
	}
}
