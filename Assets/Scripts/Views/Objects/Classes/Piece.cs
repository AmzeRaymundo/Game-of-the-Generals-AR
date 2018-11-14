using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece {

	//properties of Piece
	private GameObject pieceGameObject;
	private string pieceName;
	private string pieceColor;
	private int pieceRank;
	private int posX, posY;
	private bool isSelected;
	private int prevPosX = 0;
	private int prevPosY = 0;

	//constructor
	public Piece(GameObject pieceGameObject){
		this.pieceName = pieceGameObject.name.Substring (6);
		this.pieceGameObject = pieceGameObject;
		this.setPieceRank();
		this.pieceColor = pieceGameObject.transform.parent.name.Substring (17);
		this.isSelected = false;

		if (this.pieceColor == "White") {
			this.posX = 0;
			this.posY = 0;
		} 
		else {
			this.posX = 8;
			this.posY = 7;
		}
	}

	public void initializePiecePositionAtStart(int posX, int posY){
		this.setPiecePosition (posX, posY);

		float initPosX = this.pieceGameObject.transform.position.x;
		float initPosZ = this.pieceGameObject.transform.position.z;

		if (this.pieceColor == "White") {
			for (float i = 0; i < posX; i++) {
				initPosX += 0.5f;
			}

			for (float i = 0; i < posY; i++) {
				initPosZ += 0.5f;
			}

			Vector3 newPos = new Vector3 (initPosX, this.pieceGameObject.transform.position.y, initPosZ);
			this.pieceGameObject.transform.position = newPos;

			Board.Instance.getTile (this.posX, this.posY).setOccupiedByPlayer1 (true);
		} 

		else {
			for (float i = 8; i > posX; i--) {
				initPosX -= 0.5f;
			}

			for (float i = 7; i > posY; i--) {
				initPosZ -= 0.5f;
			}

			Vector3 newPos = new Vector3 (initPosX, this.pieceGameObject.transform.position.y, initPosZ);
			this.pieceGameObject.transform.position = newPos;

			Board.Instance.getTile (this.posX, this.posY).setOccupiedByPlayer2 (true);
		}
	}

	public void setPiecePosition(int posX, int posY){
		this.posX = posX;
		this.posY = posY;

		if (this.getPieceColor () == "White") {
			Board.Instance.getTile (posX, posY).setOccupiedByPlayer1 (true);
		} 
		else {
			Board.Instance.getTile (posX, posY).setOccupiedByPlayer2 (true);
		}
	}


	//Getters and Setters
	public GameObject getPieceGameObject(){
		return this.pieceGameObject;
	}

	public int getPieceRank(){
		return this.pieceRank;
	}
	//--------------------------------------------------------------
	public string getPieceName(){
		return this.pieceName;	
	}
	//--------------------------------------------------------------
	public string getPieceColor(){
		return this.pieceColor;	
	}
	//--------------------------------------------------------------
	public int getPosX(){
		return this.posX;
	}
	public void setPosX(int posX){
		this.posX = posX;
		Board.Instance.getTile (this.posX, this.posY).setOccupiedByPlayer1 (true);
	}
	//--------------------------------------------------------------
	public int getPosY(){
		return this.posY;
	}
	public void setPosY(int posY){
		this.posY = posY;
		Board.Instance.getTile (this.posX, this.posY).setOccupiedByPlayer1 (true);
	}
	//--------------------------------------------------------------
	public int getPrevPosX(){
		return this.posX;
	}
	public void setPrevPosX(int prevPosX){
		this.prevPosX = prevPosX;
	}
	//--------------------------------------------------------------
	public int getPrevPosY(){
		return this.prevPosY;
	}
	public void setPrevPosY(int prevPosY){
		this.prevPosY = prevPosY;
	}



	//--------------------------------------------------------------
	public bool getSelecteed(){
		return this.isSelected;
	}
	public void setSelected( bool isSelected){
		this.isSelected = isSelected;
	}
	//--------------------------------------------------------------
	private void setPieceRank(){

		if (this.pieceName == "Flag") 				 this.pieceRank = PieceRanks.FLAG;
		else if (this.pieceName.Contains("Private"))		 this.pieceRank = PieceRanks.PRIVATE;
		else if (this.pieceName == "Sergeant")		 this.pieceRank = PieceRanks.SERGEANT;
		else if (this.pieceName == "2nd Lt.") 		 this.pieceRank = PieceRanks.SECOND_LIEUTENANT;
		else if (this.pieceName == "1st Lt.")		 this.pieceRank = PieceRanks.FIRST_LIEUTENANT;
		else if (this.pieceName == "Captain")		 this.pieceRank = PieceRanks.CAPTAIN;
		else if (this.pieceName == "Major")			 this.pieceRank = PieceRanks.MAJOR;
		else if (this.pieceName == "Lt. Colonel")	 this.pieceRank = PieceRanks.COLONEL_LIEUTENANT;
		else if (this.pieceName == "Colonel")		 this.pieceRank = PieceRanks.COLONEL;
		else if (this.pieceName == "1 Star General") this.pieceRank = PieceRanks.ONE_STAR_GENERAL;
		else if (this.pieceName == "2 Star General") this.pieceRank = PieceRanks.TWO_STAR_GENERAL;
		else if (this.pieceName == "3 Star General") this.pieceRank = PieceRanks.THREE_STAR_GENERAL;
		else if (this.pieceName == "4 Star General") this.pieceRank = PieceRanks.FOUR_STAR_GENERAL;
		else if (this.pieceName == "5 Star General") this.pieceRank = PieceRanks.FIVE_STAR_GENERAL;
		else if (this.pieceName.Contains("Spy")) 			 this.pieceRank = PieceRanks.SPY;
	}

}
