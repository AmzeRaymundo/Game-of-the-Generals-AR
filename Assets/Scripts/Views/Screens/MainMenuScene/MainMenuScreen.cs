using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour {
	[SerializeField] GameObject tutorialScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadGameScene(){
		print ("LOADING MAIN");
		LoadManager.Instance.LoadScene(SceneNames.GAME_SCENE);
	}

	public void viewTutorial(){
		print ("TUTORIAL SCREEN IS SHOWN");
		this.tutorialScreen.SetActive (true);
	}

	public void ExitGame(){
		Application.Quit();
	}
}
