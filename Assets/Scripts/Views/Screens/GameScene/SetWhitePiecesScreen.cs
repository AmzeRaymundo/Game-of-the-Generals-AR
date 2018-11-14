using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetWhitePiecesScreen : MonoBehaviour {
	[SerializeField] private Image selectedPieceImg;
	[SerializeField] private Text selectedPieceName;
	[SerializeField] private Text selectedPieceRank;
	[SerializeField] private Button btnFinishSetup;
	[SerializeField] private GameObject nextPlayerSetup;

	private GameObject currentlySelectedPiece;
	private bool pieceIsSelected = false;
	private int count=0;
	private Vector3 initialPosition; //of Pieces
	private bool whitePiecesExist = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(GameSceneHandler.Instance.getBlackPlayer().getPieces() != null && !this.whitePiecesExist){
			float origPosX, origPosY, origPosZ;
			origPosX = GameSceneHandler.Instance.getWhitePlayerPiece ("Flag").getPieceGameObject ().transform.position.x;
			origPosY = GameSceneHandler.Instance.getWhitePlayerPiece ("Flag").getPieceGameObject ().transform.position.y;
			origPosZ = GameSceneHandler.Instance.getWhitePlayerPiece ("Flag").getPieceGameObject ().transform.position.z;

			initialPosition = new Vector3 (origPosX, origPosY, origPosZ);

			whitePiecesExist = true;
		}

		if (count == 21) {
			btnFinishSetup.gameObject.SetActive (true);
			count++;
		}
		
	}

	public void OnSelectedPieceClicked(string pieceName){
		this.selectedPieceName.text = pieceName;
		setPieceRank ();
		this.selectedPieceImg.sprite = setPieceImg ();

		this.selectedPieceName.gameObject.SetActive (true);
		this.selectedPieceRank.gameObject.SetActive (true);
		this.selectedPieceImg.gameObject.SetActive (true);
		this.pieceIsSelected = true;

		this.currentlySelectedPiece = EventSystem.current.currentSelectedGameObject.gameObject;
	}

	public void initializePiece(int val){
		int posX = val % 10;
		int posY = val / 10;
		Sprite UISprite = Resources.Load<Sprite>("Art/Sprites/noFillSprite");
		//print (posX + ", " + posY);

		Parameters param = new Parameters ();
		param.GetIntExtra("posX", posX);
		param.GetIntExtra("posY", posY);

		if(this.pieceIsSelected){
			if (EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite == null || 
				EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name == "noFillSprite"){ 
				count++;
			} 
			else {
				string pieceNameFromSelectedTile = GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (posX, posY).getPieceName ();

				GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (pieceNameFromSelectedTile).setPiecePosition (0, 0);
				GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (pieceNameFromSelectedTile).getPieceGameObject ().transform.position = initialPosition;
				GameObject.Find ("/GameSceneUIParent/Canvas/SetWhitePiecesScreen/PiecesContainer/ButtonContainer/Button " + pieceNameFromSelectedTile).SetActive (true);
			}

			EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite = setPieceImg ();

			GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (this.selectedPieceName.text).initializePiecePositionAtStart (posX, posY);
			this.currentlySelectedPiece.SetActive(false);
			this.pieceIsSelected = false;
		}
		else { //condition if the player clicks the tiles despite not selecting a piece
			if (EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite == null ||
			    EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name == "noFillSprite") { 
				Debug.Log ("Choose  piece first");
			} 
			else {
				EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("UISprite");

				string pieceNameFromSelectedTile = GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (posX, posY).getPieceName ();

				GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (pieceNameFromSelectedTile).setPiecePosition (0, 0);
				GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (pieceNameFromSelectedTile).getPieceGameObject ().transform.position = initialPosition;
				GameObject.Find ("/GameSceneUIParent/Canvas/SetWhitePiecesScreen/PiecesContainer/ButtonContainer/Button " + pieceNameFromSelectedTile).SetActive (true);
				Board.Instance.getTile (posX, posY).setOccupiedByPlayer1 (false);
				count--;
			}
		}
			
		this.selectedPieceName.gameObject.SetActive (false);
		this.selectedPieceRank.gameObject.SetActive (false);
		this.selectedPieceImg.gameObject.SetActive (false);

	}

	public void OnFinishSetup(){
		//load scene for scanning player 2 emblem
		this.gameObject.SetActive(false);
		EventBroadcaster.Instance.PostEvent (EventNames.ON_FINISH_TURN_WHITE);
	}


	private void setPieceRank(){

		if (this.selectedPieceName.text == "Flag") 				 	this.selectedPieceRank.text = "The flag has no rank...";
		else if (this.selectedPieceName.text.Contains("Private"))	this.selectedPieceRank.text = "Rank: " + PieceRanks.PRIVATE.ToString();
		else if (this.selectedPieceName.text == "Sergeant")			this.selectedPieceRank.text = "Rank: " + PieceRanks.SERGEANT.ToString();
		else if (this.selectedPieceName.text == "2nd Lt.") 		 	this.selectedPieceRank.text = "Rank: " + PieceRanks.SECOND_LIEUTENANT.ToString();
		else if (this.selectedPieceName.text == "1st Lt.")		 	this.selectedPieceRank.text = "Rank: " + PieceRanks.FIRST_LIEUTENANT.ToString();
		else if (this.selectedPieceName.text == "Captain")		 	this.selectedPieceRank.text = "Rank: " + PieceRanks.CAPTAIN.ToString();
		else if (this.selectedPieceName.text == "Major")			this.selectedPieceRank.text = "Rank: " + PieceRanks.MAJOR.ToString();
		else if (this.selectedPieceName.text == "Lt. Colonel")	 	this.selectedPieceRank.text = "Rank: " + PieceRanks.COLONEL_LIEUTENANT.ToString();
		else if (this.selectedPieceName.text == "Colonel")		 	this.selectedPieceRank.text = "Rank: " + PieceRanks.COLONEL.ToString();
		else if (this.selectedPieceName.text == "1 Star General") 	this.selectedPieceRank.text = "Rank: " + PieceRanks.ONE_STAR_GENERAL.ToString();
		else if (this.selectedPieceName.text == "2 Star General") 	this.selectedPieceRank.text = "Rank: " + PieceRanks.TWO_STAR_GENERAL.ToString();
		else if (this.selectedPieceName.text == "3 Star General") 	this.selectedPieceRank.text = "Rank: " + PieceRanks.THREE_STAR_GENERAL.ToString();
		else if (this.selectedPieceName.text == "4 Star General") 	this.selectedPieceRank.text = "Rank: " + PieceRanks.FOUR_STAR_GENERAL.ToString();
		else if (this.selectedPieceName.text == "5 Star General") 	this.selectedPieceRank.text = "Rank: " + PieceRanks.FIVE_STAR_GENERAL.ToString();
		else if (this.selectedPieceName.text.Contains("Spy")) 			 	this.selectedPieceRank.text = "Rank: " + PieceRanks.SPY.ToString();
	}

	private Sprite setPieceImg(){
		Sprite sprite = new Sprite ();

		if (this.selectedPieceName.text == "Flag") 				 	sprite = Resources.Load<Sprite>("Art/Sprites/W 0");
		else if (this.selectedPieceName.text.Contains("Private"))	sprite = Resources.Load<Sprite>("Art/Sprites/W 1");
		else if (this.selectedPieceName.text == "Sergeant")			sprite = Resources.Load<Sprite>("Art/Sprites/W 2");
		else if (this.selectedPieceName.text == "2nd Lt.") 		 	sprite = Resources.Load<Sprite>("Art/Sprites/W 3");
		else if (this.selectedPieceName.text == "1st Lt.")		 	sprite = Resources.Load<Sprite>("Art/Sprites/W 4");
		else if (this.selectedPieceName.text == "Captain")		 	sprite = Resources.Load<Sprite>("Art/Sprites/W 5");
		else if (this.selectedPieceName.text == "Major")			sprite = Resources.Load<Sprite>("Art/Sprites/W 6");
		else if (this.selectedPieceName.text == "Lt. Colonel")	 	sprite = Resources.Load<Sprite>("Art/Sprites/W 7");
		else if (this.selectedPieceName.text == "Colonel")		 	sprite = Resources.Load<Sprite>("Art/Sprites/W 8");
		else if (this.selectedPieceName.text == "1 Star General") 	sprite = Resources.Load<Sprite>("Art/Sprites/W 9");
		else if (this.selectedPieceName.text == "2 Star General") 	sprite = Resources.Load<Sprite>("Art/Sprites/W 10");
		else if (this.selectedPieceName.text == "3 Star General") 	sprite = Resources.Load<Sprite>("Art/Sprites/W 11");
		else if (this.selectedPieceName.text == "4 Star General") 	sprite = Resources.Load<Sprite>("Art/Sprites/W 12");
		else if (this.selectedPieceName.text == "5 Star General") 	sprite = Resources.Load<Sprite>("Art/Sprites/W 13");
		else if (this.selectedPieceName.text.Contains("Spy")) 			 	sprite = Resources.Load<Sprite>("Art/Sprites/W 14");

		return sprite;
	}

	public void randomizePositions(){
		List<int> tempStorage = new List<int>();

		while (tempStorage.Count < 21) {
			int newValY = (Random.Range (5, 8))*10;
			int newValX = Random.Range (0, 9);
			int newVal = newValX + newValY;

			if(!tempStorage.Contains(newVal)){
				tempStorage.Add (newVal);
				this.pieceIsSelected = true;
				initializePiece (newVal);
			}
		}
	}
}
