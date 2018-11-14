using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreen : MonoBehaviour {
	[SerializeField] GameObject whitePlayerEmblem, blackPlayerEmblem;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("Winner", "none") == "White") {
			whitePlayerEmblem.SetActive (true);
		} 
		else {
			blackPlayerEmblem.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadMainMenuScene(){
		print ("GOING BACK TO MAIN MENU");
		LoadManager.Instance.LoadScene (SceneNames.MAIN_MENU_SCENE);
	}

	public void LoadGameScene(){
		print ("RESTARTING GAME");
		LoadManager.Instance.LoadScene (SceneNames.GAME_SCENE);
	}
}
