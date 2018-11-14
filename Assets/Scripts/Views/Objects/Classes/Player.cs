using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Player {

	//properties of player
//	private string playerEmblem;
	private Piece[] pieces;
	private List<Piece> defeatedPieces;
	private bool isPlayerTurn;
	private string playerColor;
	private Piece lastPieceMoved;

	public Player(Piece[] pieces){
		this.pieces = pieces;
		this.playerColor = pieces [0].getPieceColor ();
		this.defeatedPieces = new List<Piece> ();
	}


	public void MovePiece(string direction, Piece selectedPiece, float moveVal){
		if (this.playerColor == "White") { 
			if (direction == "Left")
				selectedPiece.getPieceGameObject ().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x - moveVal, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z);
			else if (direction == "Right")
				selectedPiece.getPieceGameObject ().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x + moveVal, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z);
			else if (direction == "Forward")
				selectedPiece.getPieceGameObject ().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z + moveVal);
			else if (direction == "Backward")
				selectedPiece.getPieceGameObject ().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z - moveVal);
		} 
		else if (this.playerColor == "Black") { 
			if (direction == "Left")
				selectedPiece.getPieceGameObject().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x + moveVal, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z);
			else if (direction == "Right")
				selectedPiece.getPieceGameObject().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x - moveVal, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z);
			else if (direction == "Forward")
				selectedPiece.getPieceGameObject().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z - moveVal);
			else if (direction == "Backward")
				selectedPiece.getPieceGameObject().transform.localPosition = new Vector3 (selectedPiece.getPieceGameObject ().transform.localPosition.x, 0, selectedPiece.getPieceGameObject ().transform.localPosition.z + moveVal);
		}
	}

	public void attackPiece(Piece opponentPiece, Piece ownPiece){
		Debug.Log ("inside attackPiece() on Player class");
		//opponentPiece.getPieceGameObject ().SetActive (false);

		if (opponentPiece.getPieceRank () == PieceRanks.FLAG) {
			PlayerPrefs.SetString ("Winner", ownPiece.getPieceColor ());
			LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
		} 

		else if (ownPiece.getPieceRank () == PieceRanks.FLAG) {
			PlayerPrefs.SetString ("Winner", opponentPiece.getPieceColor ());
			LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
		}

		else if (opponentPiece.getPieceRank () == ownPiece.getPieceRank ()) {
			opponentPiece.getPieceGameObject ().SetActive (false);
			ownPiece.getPieceGameObject ().SetActive (false);

			this.defeatedPieces.Add (ownPiece);

			if (opponentPiece.getPieceColor () == "Black") {
				GameSceneHandler.Instance.getBlackPlayer ().addDefeatedPiece (opponentPiece);

				Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer1 (false);
				Board.Instance.getTile (opponentPiece.getPosX (), opponentPiece.getPosY ()).setOccupiedByPlayer2 (false);
			} 
			else {
				GameSceneHandler.Instance.getWhitePlayer ().addDefeatedPiece (opponentPiece);

				Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer2 (false);
				Board.Instance.getTile (opponentPiece.getPosX (), opponentPiece.getPosY ()).setOccupiedByPlayer1 (false);
			}
		} 

		else if (opponentPiece.getPieceRank () > ownPiece.getPieceRank ()) {
			if (ownPiece.getPieceRank () == 1 && opponentPiece.getPieceRank () == 14) {
				if (ownPiece.getPieceColor () == "White") {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer1 (false);
					Board.Instance.getTile (opponentPiece.getPosX (), opponentPiece.getPosY ()).setOccupiedByPlayer2 (false);
					GameSceneHandler.Instance.getBlackPlayer ().addDefeatedPiece (opponentPiece);

					if (GameSceneHandler.Instance.getBlackPlayer().getDefeatedPieces ().Count == GameSceneHandler.Instance.getBlackPlayer ().getPieces ().Length) {
						PlayerPrefs.SetString ("Winner", ownPiece.getPieceColor());
						LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
					}
				} 

				else {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer2 (false);
					Board.Instance.getTile (opponentPiece.getPosX (), opponentPiece.getPosY ()).setOccupiedByPlayer1 (false);
					GameSceneHandler.Instance.getWhitePlayer ().addDefeatedPiece (opponentPiece);

					if (GameSceneHandler.Instance.getWhitePlayer ().getDefeatedPieces ().Count == GameSceneHandler.Instance.getBlackPlayer ().getPieces ().Length) {
						PlayerPrefs.SetString ("Winner", ownPiece.getPieceColor ());
						LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
					}
				}


				Vector3 newPos = new Vector3 (opponentPiece.getPieceGameObject().transform.position.x, opponentPiece.getPieceGameObject().transform.position.y, opponentPiece.getPieceGameObject().transform.position.z);
				ownPiece.getPieceGameObject ().transform.position = newPos;
				ownPiece.setPiecePosition(opponentPiece.getPosX (), opponentPiece.getPosY());

				opponentPiece.getPieceGameObject ().SetActive (false);
			} 
			else {
				ownPiece.getPieceGameObject ().SetActive (false);
				this.defeatedPieces.Add (ownPiece);

				if (ownPiece.getPieceColor () == "White") {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer1 (false);
				} else {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer2 (false);
				}

				if (this.defeatedPieces.Count == this.pieces.Length) {
					PlayerPrefs.SetString ("Winner", opponentPiece.getPieceColor ());
					LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
				}
			}
		} 

		else if (opponentPiece.getPieceRank () < ownPiece.getPieceRank ()) {
			if (opponentPiece.getPieceRank () == 1 && ownPiece.getPieceRank () == 14) {
				ownPiece.getPieceGameObject ().SetActive (false);
				this.defeatedPieces.Add (ownPiece);

				if (ownPiece.getPieceColor () == "White") {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer1 (false);
				} else {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer2 (false);
				}

				if (this.defeatedPieces.Count == this.pieces.Length) {
					PlayerPrefs.SetString ("Winner", opponentPiece.getPieceColor ());
					LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
				}
			} 
			else {
				if (ownPiece.getPieceColor () == "White") {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer1 (false);
					Board.Instance.getTile (opponentPiece.getPosX (), opponentPiece.getPosY ()).setOccupiedByPlayer2 (false);
					GameSceneHandler.Instance.getBlackPlayer ().addDefeatedPiece (opponentPiece);

					if (GameSceneHandler.Instance.getBlackPlayer ().getDefeatedPieces ().Count == GameSceneHandler.Instance.getBlackPlayer ().getPieces ().Length) {
						PlayerPrefs.SetString ("Winner", ownPiece.getPieceColor ());
						LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
					}
				} else {
					Board.Instance.getTile (ownPiece.getPosX (), ownPiece.getPosY ()).setOccupiedByPlayer2 (false);
					Board.Instance.getTile (opponentPiece.getPosX (), opponentPiece.getPosY ()).setOccupiedByPlayer1 (false);
					GameSceneHandler.Instance.getWhitePlayer ().addDefeatedPiece (opponentPiece);

					if (GameSceneHandler.Instance.getWhitePlayer ().getDefeatedPieces ().Count == GameSceneHandler.Instance.getBlackPlayer ().getPieces ().Length) {
						PlayerPrefs.SetString ("Winner", ownPiece.getPieceColor ());
						LoadManager.Instance.LoadScene (SceneNames.RESULTS_SCENE);
					}
				}


				Vector3 newPos = new Vector3 (opponentPiece.getPieceGameObject ().transform.position.x, opponentPiece.getPieceGameObject ().transform.position.y, opponentPiece.getPieceGameObject ().transform.position.z);
				ownPiece.getPieceGameObject ().transform.position = newPos;
				ownPiece.setPiecePosition (opponentPiece.getPosX (), opponentPiece.getPosY ());


				opponentPiece.getPieceGameObject ().SetActive (false);
			}

		}

		for (int y = 0; y < 8; y++) {
			for (int x = 0; x < 9; x++) {
				Board.Instance.getTile (x, y).setActive (false);
			}
		}
	}

	public void addDefeatedPiece(Piece piece){
		this.defeatedPieces.Add (piece);
	}

	public Piece getFromPieces(string pieceName){
		for (int i = 0; i < this.pieces.Length; i++) {
			if (this.pieces [i].getPieceName () == pieceName)
				return this.pieces [i];
		}

		return null;
	}

	public Piece getFromPieces(int posX, int posY){
		for (int i = 0; i < this.pieces.Length; i++) {
			if (this.pieces [i].getPosX() == posX && this.pieces[i].getPosY() == posY)
				return this.pieces [i];
		}

		return null;
	}

	public void setPlayerPiecePosition(int pieceRank, int posX, int posY){
		for (int i = 0; i < pieces.Length; i++) {
			if (pieces [i].getPieceRank() == pieceRank)
				pieces [i].setPiecePosition (posX, posY);
				
		}
	}


	public Piece getLastPieceMoved(){
		return this.lastPieceMoved;
	}

	public void setLastPieceMoved(Piece piece){
		this.lastPieceMoved = piece;
	}

	public bool getPlayerTurn(){
		return this.isPlayerTurn;
	}

	public void setPlayerTurn(bool playerTurn){
		this.isPlayerTurn = playerTurn;
	}

	public string getPlayerColor(){
		return this.playerColor;
	}

	public void setPlayerColor(string playerColor){
		this.playerColor = playerColor;
	}

	public Piece[] getPieces(){
		return this.pieces;
	}

	public List<Piece> getDefeatedPieces(){
		return this.defeatedPieces;
	}

}
