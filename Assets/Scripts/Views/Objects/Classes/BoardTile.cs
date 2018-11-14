using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile {

	//properties of boardTiles
	private GameObject tileGameObject;
	private bool isOccupiedByPlayer1;
	private bool isOccupiedByPlayer2;
	private bool onPlayer1Territory; 			// for knowing if flag of player is at opponent's territory
	private bool onPlayer2Territory; 			// for knowing if flag of player is at opponent's territory
	private bool isActive;
	private int posX, posY;

	//contructor
	public BoardTile(GameObject tileGameObject, int posX, int posY){
		this.tileGameObject = tileGameObject;
		this.posX = posX;
		this.posY = posY;

		this.isOccupiedByPlayer1 = false;
		this.isOccupiedByPlayer2 = false;
		this.onPlayer1Territory = false;
		this.onPlayer2Territory = false;
		this.isActive = false;

		this.tileGameObject.SetActive(false);
	}

	// Getters and Setters
	public GameObject getTileGameObject(){
		return this.tileGameObject;
	}
	//--------------------------------------------------------------
	public int getPosX(){
		return this.posX;
	}
	public int getPosY(){
		return this.posY;
	}
	//--------------------------------------------------------------
	public bool getActive(){
		return this.isActive;
	}
	public void setActive(bool isActive){
		this.isActive = isActive;

 		if (this.isActive)
			this.tileGameObject.SetActive(true);
		else 
			this.tileGameObject.SetActive(false);
	}
	//--------------------------------------------------------------
	public bool occupiedByPlayer1(){
		return this.isOccupiedByPlayer1;
	}
	public void setOccupiedByPlayer1(bool isOccupiedByPlayer1){
		this.isOccupiedByPlayer1 = isOccupiedByPlayer1;
	}
	//--------------------------------------------------------------
	public bool occupiedByPlayer2(){
		return this.isOccupiedByPlayer2;
	}
	public void setOccupiedByPlayer2(bool isOccupiedByPlayer2){
		this.isOccupiedByPlayer2 = isOccupiedByPlayer2;
	}
	//--------------------------------------------------------------
	public bool isOnPlayer1Territory(){
		return this.onPlayer1Territory;
	}
	public void setPlayer1Territory(bool onPlayer1Territory){
		this.onPlayer1Territory = onPlayer1Territory;
	}
	//--------------------------------------------------------------
	public bool isOnPlayer2Territory(){
		return this.onPlayer2Territory;
	}
	public void setPlayer2Territory(bool onPlayer2Territory){
		this.onPlayer2Territory = onPlayer2Territory;
	}
}