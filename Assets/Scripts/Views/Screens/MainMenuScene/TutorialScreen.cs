using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour {
	[SerializeField] GameObject ThingsToTakeNote;
	[SerializeField] GameObject HowToPlay;
	[SerializeField] GameObject GoalOfTheGame;


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

	public void GoBackToMai(){
		this.gameObject.SetActive (false);
	}

	public void OnClickThingsToTakeNoteButton(){
		ThingsToTakeNote.SetActive (true);
		HowToPlay.SetActive (false);
		GoalOfTheGame.SetActive (false);
	}

	public void OnClickHowToPlay(){
		ThingsToTakeNote.SetActive (false);
		HowToPlay.SetActive (true);
		GoalOfTheGame.SetActive (false);
	}

	public void OnClickGoalOfTheGame(){
		ThingsToTakeNote.SetActive (false);
		HowToPlay.SetActive (false);
		GoalOfTheGame.SetActive (true);
	}
}
