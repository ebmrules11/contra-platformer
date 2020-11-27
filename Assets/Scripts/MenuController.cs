using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	
	public GameObject button_playClone;
	public GameObject button_playVariant;
	public GameObject button_mainMenu;
	
	
	
	public void playClone(){
		SceneManager.LoadScene (sceneName:"SIzzleReel");
	}
	public void playVariant(){
		SceneManager.LoadScene (sceneName:"VariantScene");
	}
	public void goToMainMenu(){
		SceneManager.LoadScene (sceneName:"MenuScene");
	}
	
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
