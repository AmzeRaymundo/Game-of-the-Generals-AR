using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlayerBehaviour : MonoBehaviour {
	[SerializeField] private Camera arCamera;
	[SerializeField] private GameObject[] pieces;

	private Piece selectedPiece;
	private bool pieceIsSelected = false;
	private bool emblemScanSuccess = false;


	// Use this for initialization
	void Start () {
		Piece[] playerPieces = new Piece[pieces.Length];

		for (int i = 0; i < pieces.Length; i++) {
			playerPieces[i] = new Piece(pieces[i]);
		}

		GameSceneHandler.Instance.setBlackPlayer(new Player(playerPieces));
		GameSceneHandler.Instance.getBlackPlayer().setPlayerTurn (false);

		for (int i = 0; i < GameSceneHandler.Instance.getBlackPlayer().getPieces().Length; i++) {
			GameSceneHandler.Instance.getBlackPlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (false);	
			GameSceneHandler.Instance.getBlackPlayer ().getPieces () [i].getPieceGameObject ().transform.GetComponent<Collider> ().enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if(GameSceneHandler.Instance.getBlackPlayerPiece("Flag").getPosY() == 0  && GameSceneHandler.Instance.getBlackPlayer().getPlayerTurn()  == true){
			Debug.Log ("Entered Flag if statement BLACK");
			PlayerPrefs.SetString ("Winner", "Black");
			LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
		}

		if (Input.GetMouseButtonDown (0) && GameSceneHandler.Instance.getBlackPlayer().getPlayerTurn () == true) {
			Ray ray = this.arCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit) && hit.collider.name.Contains ("Piece") && hit.collider.transform.parent.name.Contains ("Black")) {
				EventBroadcaster.Instance.PostEvent (EventNames.ON_PIECE_SELECTED);

				this.pieceIsSelected = true;
				this.selectedPiece = GameSceneHandler.Instance.getBlackPlayer ().getFromPieces (hit.collider.name.Substring (6));
				this.selectedPiece.setPrevPosX (this.selectedPiece.getPosX());
				this.selectedPiece.setPrevPosY (this.selectedPiece.getPosY());

				//when clicks a piece, set all tiles to false at the start
				for (int y = 0; y < 8; y++) {
					for (int x = 0; x < 9; x++) {
						Color32 red = new Color32 ();
						red.r = 104;
						red.g = 25;
						red.b = 25;

						Board.Instance.getTile (x, y).getTileGameObject ().GetComponent<Collider> ().enabled = true;
						Board.Instance.getTile (x, y).getTileGameObject ().GetComponent<Renderer> ().material.color = red;
						Board.Instance.getTile (x, y).setActive (false);

//						ParticleSystem ps = Board.Instance.getTile (x, y).getTileGameObject ().transform.GetChild (0).GetComponent<ParticleSystem>();
//						ps.main.startColor = new Color (104f, 25f, 25f, 1f);
					}
				}

				//setting the needed tiles active...
				/*Right*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()) && !Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).occupiedByPlayer2 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).setActive (true);

					if (Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY () ).occupiedByPlayer1()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: FORWARD");
						Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY () ).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}

				}
				/*Forward*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1) && !Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).occupiedByPlayer2 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).setActive (true);

					if (Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).occupiedByPlayer1()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: FORWARD");
						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () - 1).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}
				}
				/*Left*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()) && !Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).occupiedByPlayer2 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).setActive (true);

					if (Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).occupiedByPlayer1()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: FORWARD");
						Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}
				}
				/*Backward*/
				if (Board.Instance.tileExist (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1) && !Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).occupiedByPlayer2 ()) {
					Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).setActive (true);

					if (Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).occupiedByPlayer1()) {
						print ("PARTICLE SYSTEMS SHOULD BE SHOWN: FORWARD");
						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY () + 1).getTileGameObject ().transform.GetChild (0).gameObject.SetActive (true);
					}
				}
			} 

			else if (Physics.Raycast (ray, out hit) && hit.collider.name.Contains ("TileBoard") && this.pieceIsSelected == true) {
				Board.Instance.getTile (selectedPiece.getPosX (), selectedPiece.getPosY ()).setOccupiedByPlayer2 (false);

				for (int y = 0; y < 8; y++) {
					for (int x = 0; x < 9; x++) {
						if (Board.Instance.getTile (x, y).getTileGameObject ().name == hit.collider.name) {
							if (Board.Instance.getTile (x, y).occupiedByPlayer1 ()) {
								GameSceneHandler.Instance.getBlackPlayer ().attackPiece (GameSceneHandler.Instance.getWhitePlayerPiece (x, y), this.selectedPiece);
								print ("The tile was clicked....PLAYER 2 HAS ENCOUNTERED AN ENEMY");
							}

							else if(y==0 && !Board.Instance.getTile(x+1, y).occupiedByPlayer1() && !Board.Instance.getTile(x-1, y).occupiedByPlayer1() && this.selectedPiece.getPieceName() == "Flag"){
								PlayerPrefs.SetString ("Winner", "Black");
								LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
							}

							else if (!Board.Instance.getTile (x, y).occupiedByPlayer1()) {
								print (x + ":" + this.selectedPiece.getPosX ());
								if (x > this.selectedPiece.getPosX ()) {
									GameSceneHandler.Instance.getBlackPlayer ().MovePiece ("Left", this.selectedPiece, 0.5f);
								} else if (x < this.selectedPiece.getPosX ()) {
									GameSceneHandler.Instance.getBlackPlayer ().MovePiece ("Right", this.selectedPiece, 0.5f);
								} else if (y > this.selectedPiece.getPosY ()) {
									GameSceneHandler.Instance.getBlackPlayer ().MovePiece ("Backward", this.selectedPiece, 0.5f);
								} else if (y < this.selectedPiece.getPosY ()) {
									GameSceneHandler.Instance.getBlackPlayer ().MovePiece ("Forward", this.selectedPiece, 0.5f);
								}
								this.selectedPiece.setPiecePosition (x, y);
								Board.Instance.getTile (x, y).setOccupiedByPlayer2 (true);
								EventBroadcaster.Instance.PostEvent (EventNames.ON_PIECE_MOVE);
							}

							if (Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY ()).isOnPlayer1Territory () && this.selectedPiece.getPieceName() == "Flag") {
								if (!Board.Instance.getTile (this.selectedPiece.getPosX () - 1, this.selectedPiece.getPosY ()).occupiedByPlayer1 () &&
									!Board.Instance.getTile (this.selectedPiece.getPosX () + 1, this.selectedPiece.getPosY ()).occupiedByPlayer1 ()) {

									 PlayerPrefs.SetString ("Winner", "Black");
									LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
								}
							}
						}
						Board.Instance.getTile (x, y).setActive (false);

						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY ()).setActive(true);
						Board.Instance.getTile (this.selectedPiece.getPosX (), this.selectedPiece.getPosY ()).getTileGameObject().transform.GetComponent<Collider>().enabled = false;

						Board.Instance.getTile (this.selectedPiece.getPrevPosX (), this.selectedPiece.getPrevPosY ()).setActive(true);
						Board.Instance.getTile (this.selectedPiece.getPrevPosX (), this.selectedPiece.getPrevPosY()).setOccupiedByPlayer2(false);
						Board.Instance.getTile (this.selectedPiece.getPrevPosX (), this.selectedPiece.getPrevPosY ()).getTileGameObject().transform.GetComponent<Collider>().enabled = false;
					}
				}
				this.EndOfTurn ();
			} 

			else if ((Physics.Raycast (ray, out hit) && hit.collider.name.Contains ("Piece") && hit.collider.transform.parent.name.Contains ("White")) && this.pieceIsSelected == true) {
				EventBroadcaster.Instance.PostEvent (EventNames.ON_PIECE_MOVE);
				if(	(GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() +1 && GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY()) ||
					(GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() -1 && GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY()) ||
					(GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() && GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY() +1 ) ||
					(GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosX() == this.selectedPiece.getPosX() && GameSceneHandler.Instance.getWhitePlayerPiece(hit.collider.name.Substring (6)).getPosY() == this.selectedPiece.getPosY() -1 ) ){

					GameSceneHandler.Instance.getBlackPlayer ().attackPiece (GameSceneHandler.Instance.getWhitePlayerPiece (hit.collider.name.Substring (6)), this.selectedPiece);
					print ("The Piece itself was clicked....PLAYER 2 HAS ENCOUNTERRED AN ENEMY");
					EventBroadcaster.Instance.PostEvent (EventNames.ON_PIECE_MOVE);
					EventBroadcaster.Instance.PostEvent (EventNames.ON_PLAYER_ATTACK);

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
//		GameSceneHandler.Instance.getBlackPlayer ().setPlayerTurn (false);
//		GameSceneHandler.Instance.getWhitePlayer ().setPlayerTurn (true);
//		for (int i = 0; i < GameSceneHandler.Instance.getWhitePlayer().getPieces().Length; i++) {
//			GameSceneHandler.Instance.getWhitePlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
//			GameSceneHandler.Instance.getBlackPlayer ().getPieces()[i].getPieceGameObject ().transform.GetChild (0).GetChild (0).gameObject.SetActive (false);	
//		}
		EventBroadcaster.Instance.PostEvent(EventNames.ON_END_TURN);
		this.pieceIsSelected = false;
	}
}
