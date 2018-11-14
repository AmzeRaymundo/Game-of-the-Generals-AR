using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneHandler {
	private static GameSceneHandler sharedInstance;

	public static GameSceneHandler Instance{
		get{ 
			if (sharedInstance == null) {
				sharedInstance = new GameSceneHandler ();
			}

			return sharedInstance;
		}
	}

	//properties of the GameSceneHandler
	private Player whitePlayer;
	private Player blackPlayer;

	private bool blackInitialized = false;
	private bool whiteInitialized = false;

	public Piece getBlackPlayerPiece(string pieceName){
		return this.blackPlayer.getFromPieces(pieceName);
	}

	public Piece getBlackPlayerPiece(int posX, int posY){
		return this.blackPlayer.getFromPieces(posX, posY);
	}

	public Piece getWhitePlayerPiece(string pieceName){
		return this.whitePlayer.getFromPieces(pieceName);
	}

	public Piece getWhitePlayerPiece(int posX, int posY){
		return this.whitePlayer.getFromPieces(posX, posY);
	}

	//getters & setters
	public Player getWhitePlayer(){
		return this.whitePlayer;
	}
	public void setWhitePlayer(Player whitePlayer){
		this.whitePlayer = whitePlayer;
	}

	public Player getBlackPlayer(){
		return this.blackPlayer;
	}
	public void setBlackPlayer(Player blackPlayer){
		this.blackPlayer = blackPlayer;
	}


	public bool getBlackInitialized(){
		return this.blackInitialized;
	}
	public void setBlackInitialized(bool blackInitialized){
		this.blackInitialized = blackInitialized;
	}

	public bool getWhiteInitialized(){
		return this.whiteInitialized;
	}
	public void setWhiteInitialized(bool whiteInitialized){
		this.whiteInitialized = whiteInitialized;
	}
}
