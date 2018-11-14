using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepScreen : MonoBehaviour {
	[SerializeField] Text msg;
	[SerializeField] Image whiteEmblem;
	[SerializeField] Image blackEmblem;
	[SerializeField] Image mainEmblem;
	[SerializeField] GameObject whiteSetup, blackSetup;
	[SerializeField] GameObject nextScreen;

	private bool isWhitePlayerHidden = false;
	private bool isBlackPlayerHidden = false;

	private bool scanWhiteSuccess = false;
	private bool scanBlackSuccess = false;
	private bool mainSuccess = false;

	private bool whitePiecesInitialized = false;
	private bool blackPiecesInitialized = false;

	// Use this for initialization
	void Start () {
		
	}
	
	void Awake(){
		EventBroadcaster.Instance.AddObserver (EventNames.ON_WHITE_LOGO_SCAN, this.OnWhiteLogoScan);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_BLACK_LOGO_SCAN, this.OnBlackLogoScan);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_FINISH_TURN_WHITE, this.OnFinishTurnWhite);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_FINISH_TURN_BLACK, this.OnFinishTurnBlack);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_RETURN_TO_DEFAULT_MSG, this.OnReturnToDefaultMsg);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_MAIN_TARGET_SCAN, this.OnMainTargetScanned);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnDestroy(){
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_WHITE_LOGO_SCAN);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_BLACK_LOGO_SCAN);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_FINISH_TURN_WHITE);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_FINISH_TURN_BLACK);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_RETURN_TO_DEFAULT_MSG);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_MAIN_TARGET_SCAN);
	}

	public void OnWhiteLogoScan(){
		if (!this.scanWhiteSuccess) {
			this.scanWhiteSuccess = true;
			this.scanBlackSuccess = false;

			this.isWhitePlayerHidden = false;
			this.isBlackPlayerHidden = true;

			if (!whitePiecesInitialized && !blackPiecesInitialized) {
				this.gameObject.SetActive (false);
				this.whiteSetup.SetActive (true);
			}
			else {
				msg.text = "You're Scanning the wrong emblem my friend...";
			}

		}
	}

	public void OnFinishTurnWhite(){
		if (!whitePiecesInitialized) {
			whitePiecesInitialized = true;
		}

		this.gameObject.SetActive (true);
		this.whiteEmblem.gameObject.SetActive(false);
		this.blackEmblem.gameObject.SetActive(true);
		GameSceneHandler.Instance.setWhiteInitialized (true);
	}

	public void OnBlackLogoScan(){
		if (!this.scanBlackSuccess) {
			this.scanBlackSuccess = true;
			this.scanWhiteSuccess = false;

			this.isBlackPlayerHidden = false;
			this.isWhitePlayerHidden = true;

			if (!blackPiecesInitialized && whitePiecesInitialized) {
				this.blackSetup.SetActive (true);
				this.gameObject.SetActive (false);
			}
			else{
				msg.text = "You're Scanning the wrong emblem my friend...";
			}
		}
	}

	public void OnFinishTurnBlack(){
		if (!blackPiecesInitialized) {
			blackPiecesInitialized = true;

			this.gameObject.SetActive (true);
			this.blackEmblem.gameObject.SetActive(false);
			this.whiteEmblem.gameObject.SetActive(true);
		}

		if (blackPiecesInitialized && whitePiecesInitialized) {
			this.blackEmblem.gameObject.SetActive(false);
			this.whiteEmblem.gameObject.SetActive(false);
			this.mainEmblem.gameObject.SetActive (true);
			this.msg.text = "Setups of both players have been initialized. Scan the emblem above to start the game. The one who setup first will go first.";
			GameSceneHandler.Instance.setBlackInitialized (true);
		}
	}

	public void OnReturnToDefaultMsg(){
		if(this.isBlackPlayerHidden == false){
			this.scanBlackSuccess = false;

			this.msg.text = "Whoever holds this emblem please scan to setup your pieces.";
			this.isBlackPlayerHidden = true;
		}
		else if(this.isWhitePlayerHidden == false){
			this.scanWhiteSuccess = false;

			this.msg.text = "Whoever holds this emblem please scan to setup your pieces.";
			this.isWhitePlayerHidden = true;
		}
	}

	public void OnMainTargetScanned(){
		if (this.mainSuccess == false && this.blackPiecesInitialized && this.whitePiecesInitialized) {
			this.mainSuccess = true;

			this.gameObject.SetActive (false);
			this.nextScreen.SetActive (true);
		} 
		else {
			msg.text = "You're Scanning the wrong emblem my friend...";
		}
	}
}
