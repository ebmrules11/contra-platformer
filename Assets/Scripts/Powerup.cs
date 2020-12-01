using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float maxSpeed = .1f;
    public float amplitude = .1f;
	
	// possible values: "Movement Speed", "Rapid Fire", "Extra Life", "Buckshot"
	public string effect;
	
	// how long the effect will last
	public float effectDuration;
	
	
	public IEnumerator applyEffect(GameObject targetPlayer){
		
		PlayerController pc = targetPlayer.GetComponent<PlayerController>();
		
		if(effect == "Movement Speed"){
			float originalVal = pc.moveSpeed;
			pc.moveSpeed *= 2f;
			pc.powerupText.text = "x2 MOVEMENT SPEED";
			yield return new WaitForSeconds(effectDuration);
			pc.moveSpeed = originalVal;
			pc.powerupText.text = "";
		}
		else if(effect == "Rapid Fire"){
			float originalVal = pc.fireCooldown;
			pc.fireCooldown *= .25f;
			pc.powerupText.text = "RAPID FIRE";
			yield return new WaitForSeconds(effectDuration);
			pc.fireCooldown = originalVal;
			pc.powerupText.text = "";
		}
		// this one is permanent, doesn't take effect duration
		else if(effect == "Extra Life"){
			pc.lives++;
			pc.setLifePanel(pc.lives);
			pc.powerupText.text = "+1 LIFE";
			yield return new WaitForSeconds(1f);
			pc.powerupText.text = "";
		}
		else if(effect == "Buckshot"){
			pc.bullet = pc.bulletPrefab_buckshot;
			pc.powerupText.text = "BUCKSHOT FIRE";
			yield return new WaitForSeconds(effectDuration);
			pc.bullet = pc.bulletPrefab_regular;
			pc.powerupText.text = "";
		}
		
		
	}
	
    // Start is called before the first frame update
    void Start()
    {
    }

    
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + amplitude * Mathf.Sin(Time.time * maxSpeed), 0);
    }
}
