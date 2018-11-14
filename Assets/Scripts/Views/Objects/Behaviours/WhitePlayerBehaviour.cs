using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePlayerBehaviour : MonoBehaviour {
	[SerializeField] private Camera arCamera;
	[SerializeField] private GameObject[] pieces;

	private Piece selectedPiece;
	private bool pieceIsSelected= false;
	private bool emblemScanSuccess = false;

	// Use this for initialization
	void Start () {
		Piece[] playerPieces = new Piece[pieces.Length];

		for (int i = 0; i < pieces.Length; i++) {
			playerPieces[i] = new Piece(pieces[i]);
		}

		GameSceneHandler.Instance.setWhitePlayer(new Player(playerPieces));
		GameSceneHandler.Instance.getWhitePlayer().setPlayerTurn (false);

		for (int i = 0; i < GameSceneHandler.Instance.getWhitePlayer().getPieces().Length; i++) {
			GameSceneHandler.Instance.getWhitePlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (false);	
			GameSceneHandler.Instance.getWhitePlayer ().getPieces () [i].getPieceGameObject ().transform.GetComponent<Collider> ().enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(GameSceneHandler.Instance.getWhitePlayerPiece("Flag").getPosY() == 7 && GameSceneHandler.Instance.getWhitePlayer().getPlayerTurn()  == true){
			Debug.Log ("Entered Flag if statement WHITE");
			PlayerPrefs.SetString ("Winner", "White");
			LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
		}

		if (Input.GetMouseButtonDown (0) && GameSceneHandler.Instance.getWhitePlayer().getPlayerTurn()  == true) {
			Ray ray = this.arCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit) && hit.collider.name.Contains ("Piece") && hit.collider.transform.parent.name.Contains ("White")) {
				EventBroadcaster.Instance.PostEvent (EventNames.ON_PIECE_SELECTED);

				this.pieceIsSelected = true;
				this.selectedPiece = GameSceneHandler.Instance.getWhitePlayer ().getFromPieces (hit.collider.name.Substring (6));
				this.selectedPiece.setPrevPosX (this.selectedPiece.getPosX());
				this.selectedPiece.setPrevPosY (this.selectedPiece.getPosY());

				//when clicks a piece, set all tiles to false at the start
				for (int y = 0; y < 8; y++) {
					for (int x = 0; x < 9; x++) {
						Color32 blue = new Color32 ();
						blue.r = 15;
						blue.g = 100;
						blue.b = 137;

						Board.Instance.getTile (x, y).getTileGameObject ().GetComponent<Collider> ().enabled = true;
			//			Board.Instance.getTile (x, y).getTileGameObject ().transform.GetChild (0).GetComponent<ParticleSystem>().main.startColor = blue;
						Board.Instance.getTile (x, y).getTileGameObject ().GetComponent<Renderer> ().material.color = blue;
						Board.Instance.getTile (x, y).setActive (false);
					}
				}

				//setting the needed tiles active...
				/*Right*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()) && !Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).occupiedByPlayer1 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).setActive (true);
					if (Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).occupiedByPlayer2()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: RIGHT");
						Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}
				}
				/*Forward*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1) && !Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).occupiedByPlayer1 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).setActive (true);

					if (Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).occupiedByPlayer2()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: FORWARD");
						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}
				}
				/*Left*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()) && !Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).occupiedByPlayer1 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).setActive (true);

					if (Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).occupiedByPlayer2()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: LEFT");
						Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}
				}
				/*Backward*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1) && !Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).occupiedByPlayer1 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).setActive (true);

					if (Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).occupiedByPlayer2()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: BACK");
						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}
				}
			}  

			else if (Physics.Raycast (ray, out hit) && hit.collider.name.Contains ("TileBoard") && this.pieceIsSelected == true) {
				Board.Instance.getTile (selectedPiece.getPosX (), selectedPiece.getPosY ()).setOccupiedByPlayer1 (false);
				for (int y = 0; y < 8; y++) {
					for (int x = 0; x < 9; x++) {	
						if (Board.Instance.getTile (x, y).getTileGameObject ().name == hit.collider.name) {
							print ("HIT " + " AT " + x + ":" + y);
							if (Board.Instance.getTile (x, y).occupiedByPlayer2 ()) {
								//attack method
								GameSceneHandler.Instance.getWhitePlayer().attackPiece(GameSceneHandler.Instance.getBlackPlayerPiece(x, y), this.selectedPiece);
								print ("PLAYER 1 HAS ENCOUNTERED AN ENEMY");
							}

							else if(y==7 && !Board.Instance.getTile(x+1, y).occupiedByPlayer2() && !Board.Instance.getTile(x-1, y).occupiedByPlayer2() && this.selectedPiece.getPieceName() == "Flag"){
								print (this.selectedPiece.getPieceName());
								PlayerPrefs.SetString ("Winner", "White");
								LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
							}

							else if (!Board.Instance.getTile (x, y).occupiedByPlayer2 ()) {

								if (x > this.selectedPiece.getPosX ()) {
									GameSceneHandler.Instance.getWhitePlayer().MovePiece ("Right", this.selectedPiece, 0.5f);
								} else if (x < this.selectedPiece.getPosX ()) {
									GameSceneHandler.Instance.getWhitePlayer().MovePiece ("Left", this.selectedPiece, 0.5f);
								} else if (y > this.selectedPiece.getPosY ()) {
									GameSceneHandler.Instance.getWhitePlayer().MovePiece ("Forward", this.selectedPiece, 0.5f);
								} else if (y < this.selectedPiece.getPosY ()) {
									GameSceneHandler.Instance.getWhitePlayer().MovePiece ("Backward", this.selectedPiece, 0.5f);
								}
								this.selectedPiece.setPiecePosition (x, y);
								Board.Instance.getTile (x, y).setOccupiedByPlayer1 (true);
								EventBroadcaster.Instance.PostEvent (EventNames.ON_PIECE_MOVE);


							} 

							if (Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY ()).isOnPlayer2Territory () && this.selectedPiece.getPieceName() == "Flag") {
								if (!Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).occupiedByPlayer2 () &&
								    !Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).occupiedByPlayer2 ()) {

									PlayerPrefs.SetString ("Winner", "White");
									LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
								}
							}

						}
						Board.Instance.getTile (x, y).setActive (false);

						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY ()).setActive(true);
						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY ()).getTileGameObject().transform.GetComponent<Collider>().enabled = false;

						Board.Instance.getTile (this.selectedPiece.getPrevPosX (), this.selectedPiece.getPrevPosY ()).setActive(true);
						Board.Instance.getTile (this.selectedPiece.getPrevPosX (), this.selectedPiece.getPrevPosY()).setOccupiedByPlayer1(false);
						Board.Instance.getTile (this.selectedPiece.getPrevPosX (), this.selectedPiece.getPrevPosY ()).getTileGameObject().transform.GetComponent<Collider>().enabled = false;
					}
				}
				this.EndOfTurn ();
				//EventBroadcaster.Instance.PostEvent (EventNames.ON_END_TURN);
			} 

			else if ( (Physics.Raycast (ray, out hit) && hit.collider.name.Contains ("Piece") &&  hit.collider.transform.parent.name.Contains("Black")) && this.pieceIsSelected == true) {
				if(	(GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() +1 && GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY()) ||
					(GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() -1 && GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY()) ||
					(GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() && GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY() +1 ) ||
					(GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() && GameSceneHandler.Instance.getBlackPlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY() -1 ) ){


					EventBroadcaster.Instance.PostEvent (EventNames.ON_PLAYER_ATTACK);
					GameSceneHandler.Instance.getWhitePlayer ().attackPiece (GameSceneHandler.Instance.getBlackPlayerPiece (hit.collider.name.Substring (6)), this.selectedPiece);
					print ("PLAYER 1 HAS ENCOUNTERED AN ENEMY");
					EventBroadcaster.Instance.PostEvent (EventNames.ON_PIECE_MOVE);
					this.EndOfTurn ();
					Board.Instance.getTile (this.selectedPiece.getPrevPosX() ,this.selectedPiece.getPrevPosY()).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (false);
				} 

				else {
					print ("CANNOT ATTACK PIECE");
				}
			}
		}
	}

	public void EndOfTurn(){
//		GameSceneHandler.Instance.getWhitePlayer ().setPlayerTurn (false);
//		for (int i = 0; i < GameSceneHandler.Instance.getBlackPlayer().getPieces().Length; i++) {
//			GameSceneHandler.Instance.getBlackPlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
//			GameSceneHandler.Instance.getWhitePlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (false);	
//		}
		EventBroadcaster.Instance.PostEvent(EventNames.ON_END_TURN);
		this.pieceIsSelected = false;
	}
}
