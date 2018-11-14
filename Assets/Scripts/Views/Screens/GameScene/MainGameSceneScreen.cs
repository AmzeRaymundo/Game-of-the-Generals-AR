using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameSceneScreen : MonoBehaviour {
	[SerializeField] private Image whiteEmblem;
	[SerializeField] private Image blackEmblem;
//	[SerializeField] private Image surrenderEmblem;
	[SerializeField] private GameObject textPanel;
	[SerializeField] private GameObject choosePiece;
	[SerializeField] private GameObject movePiece;
	[SerializeField] private GameObject pieceRankings;
	[SerializeField] private GameObject pieceRankingButton;
	[SerializeField] private Image turnIndicator;
	[SerializeField] private GameObject attackNotifier;

	private Sprite choosePieceCheck;
	private Sprite movePieceCheck;
//	private Sprite pieceCheck = Resources.Load<Sprite>;

	private bool isWhiteEmblemScanned = false;
	private bool isBlackEmblemScanned = false;
	private bool surrenderScanSuccess = false;
	private bool playerAttacked = false;


	// Use this for initialization
	void Start () {
	}

	void Awake(){
		EventBroadcaster.Instance.AddObserver (EventNames.ON_PIECE_SELECTED, this.OnPieceSelected);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_PIECE_MOVE, this.OnPieceMove);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_END_TURN, this.OnEndTurn);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_SCAN_SURRENDER, this.OnScanSurrender);

		EventBroadcaster.Instance.AddObserver (EventNames.ON_WHITE_TURN_ACTIVATE, this.OnWhiteTurnActivate);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_BLACK_TURN_ACTIVATE, this.OnBlackTurnActivate);

		EventBroadcaster.Instance.AddObserver (EventNames.ON_PLAYER_ATTACK, this.OnPlayerAttack);
	}

	void OnDestroy(){
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_PIECE_SELECTED);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_PIECE_MOVE);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_END_TURN);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_SCAN_SURRENDER);

		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_WHITE_TURN_ACTIVATE);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_BLACK_TURN_ACTIVATE);

		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_PLAYER_ATTACK);
	}

	// Update is called once per frame
	void Update () {
		
	}	

	public void OnPieceSelected(){
		Debug.Log("WNET INSIDE ON PIECE SELECED Method");
		this.choosePiece.gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Art/Sprites/checkMarkColored");
	}

	public void OnPieceMove(){
		Debug.Log("WNET INSINDE ON PIECE MOVE Method");
		this.movePiece.gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Art/Sprites/checkMarkColored");
	}

	public void OnPlayerAttack(){
		string pieceAttacked;
		//string pieceAttackedColor;
		string resultOfAttack;

		//		if (resultOfAttack == "won") {

		//		}
		Debug.Log("entered OnPlayerAttack");
		this.playerAttacked = true;
	}

	public void OnEndTurn(){
		this.choosePiece.SetActive (false);
		this.movePiece.SetActive(false);
		this.attackNotifier.SetActive(false);
		this.turnIndicator.transform.parent.gameObject.SetActive (false);
		this.pieceRankingButton.SetActive (false);
		this.choosePiece.gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Art/Sprites/checkMarkWhite");
		this.movePiece.gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Art/Sprites/checkMarkWhite");


		if (this.isWhiteEmblemScanned && !this.isBlackEmblemScanned) {
			this.isWhiteEmblemScanned = false;

			this.blackEmblem.gameObject.SetActive (true);
			this.textPanel.SetActive (true);
		} 
		else if(!this.isWhiteEmblemScanned && this.isBlackEmblemScanned) {
			this.isBlackEmblemScanned = false;

			this.whiteEmblem.gameObject.SetActive (true);
			this.textPanel.SetActive (true);
		}

		for (int i = 0; i < GameSceneHandler.Instance.getBlackPlayer().getPieces().Length; i++) {
			GameSceneHandler.Instance.getBlackPlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
			GameSceneHandler.Instance.getWhitePlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (false);	

			GameSceneHandler.Instance.getBlackPlayer ().getPieces () [i].getPieceGameObject ().transform.GetComponent<Collider> ().enabled = false;
			GameSceneHandler.Instance.getWhitePlayer ().getPieces () [i].getPieceGameObject ().transform.GetComponent<Collider> ().enabled = false;
		}

		for (int y = 0; y < 8 ; y++) {
			for (int x = 0; x < 9; x++) {
				Board.Instance.getTile (x,y).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (false);
			}
		}

		this.isBlackEmblemScanned = false;
		this.isWhiteEmblemScanned = false;

	}

	public void OnWhiteTurnActivate(){
		if (!this.isWhiteEmblemScanned && this.whiteEmblem.IsActive()) {
			this.whiteEmblem.gameObject.SetActive (false);
			this.textPanel.gameObject.SetActive (false);

			if (this.playerAttacked) {
				this.attackNotifier.SetActive (true);
				this.playerAttacked = false;
			}
			this.turnIndicator.transform.parent.gameObject.SetActive (true);
			this.turnIndicator.sprite = Resources.Load<Sprite> ("Art/Sprites/White Player Emblem - Thin");

			this.choosePiece.SetActive (true);
			this.movePiece.SetActive(true);
			this.pieceRankingButton.SetActive (true);
			this.isWhiteEmblemScanned = true;


			GameSceneHandler.Instance.getWhitePlayer ().setPlayerTurn (true);
			GameSceneHandler.Instance.getBlackPlayer ().setPlayerTurn (false);
			for (int i = 0; i < GameSceneHandler.Instance.getWhitePlayer().getPieces().Length; i++) {
				GameSceneHandler.Instance.getWhitePlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (true);	
				GameSceneHandler.Instance.getWhitePlayer ().getPieces () [i].getPieceGameObject ().transform.GetComponent<Collider> ().enabled = true;
			}
		}
	}

	public void OnBlackTurnActivate(){
		if (!this.isBlackEmblemScanned && this.blackEmblem.IsActive()) {
			this.blackEmblem.gameObject.SetActive (false);
			this.textPanel.gameObject.SetActive (false);

			if (this.playerAttacked) {
				this.attackNotifier.SetActive (true);
				this.playerAttacked = false;
			}
			this.turnIndicator.transform.parent.gameObject.SetActive (true);
			this.turnIndicator.sprite = Resources.Load<Sprite> ("Art/Sprites/Black Player Emblem - Thin");

			this.choosePiece.SetActive (true);
			this.movePiece.SetActive(true);
			this.pieceRankingButton.SetActive (true);
			this.isBlackEmblemScanned = true;

			GameSceneHandler.Instance.getBlackPlayer ().setPlayerTurn (true);
			GameSceneHandler.Instance.getWhitePlayer ().setPlayerTurn (false);
			for (int i = 0; i < GameSceneHandler.Instance.getBlackPlayer().getPieces().Length; i++) {
				GameSceneHandler.Instance.getBlackPlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (true);	
				GameSceneHandler.Instance.getBlackPlayer ().getPieces () [i].getPieceGameObject ().transform.GetComponent<Collider> ().enabled = true;
			}
		}
	}
		
	public void viewPieceRankings(){
		this.pieceRankings.SetActive (true);
	}

	public void OnScanSurrender(){
		if (!this.surrenderScanSuccess) {
			surrenderScanSuccess = true;

			if (GameSceneHandler.Instance.getBlackPlayer ().getPlayerTurn ()) {
				PlayerPrefs.SetString ("Winner", "White");
				LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
			} 
			else if(GameSceneHandler.Instance.getWhitePlayer ().getPlayerTurn ()) {
				PlayerPrefs.SetString ("Winner", "Black");
				LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);				
			}
		}
	}
}

