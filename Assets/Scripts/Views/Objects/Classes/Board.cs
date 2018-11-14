using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

	private static Board sharedInstance = null;
	public static Board Instance {
		get {
			if (sharedInstance == null) {
				sharedInstance = new Board ();
			}
			return sharedInstance;
		}
	}

	//properties of board
	private float pieceMoveVal = 0.5f;
	private GameObject boardGameObject = new GameObject();
	private int xLength = 9;
	private int yLength = 8;
	private BoardTile[,] tiles;

	public bool tileExist(int posX, int posY){
		if(posX < 0 || posY < 0  || posX > 8 || posY > 7)
			return false;
		else
			return true;
	}


	// getters and setters of the properties
	public int getXLength(){
		return this.xLength;
	}
	public int getYLength(){
		return this.yLength;
	}

	public void setBoardGameObject(GameObject boardGameObject){
		this.boardGameObject = boardGameObject;
	}

	public void setTiles(GameObject[] tileGameObjects){
		this.tiles = new BoardTile[xLength, yLength];

		int index = 0;

		for (int y=0; y<yLength; y++) {
			for (int x=0; x<xLength; x++) {
				BoardTile tile = new BoardTile(tileGameObjects[index], x, y);
				this.tiles[x,y] = tile;

				if (y == 0)
					this.tiles [x,y].setPlayer1Territory (true);
				else if (y == 7)
					this.tiles [x,y].setPlayer2Territory (true);

				index++;
			}
		}
	}

	public BoardTile[,] getTiles(){
		return this.tiles;
	}

	public BoardTile getTile(int posX, int posY){
		if (posX > 8)
			posX = 8;
		else if (posX < 0)
			posX = 0;

		if (posY > 7)
			posY = 7;
		else if (posY < 0)
			posY = 0;
		
		return this.tiles [posX, posY];
	}

	public float getPieceMoveVal(){
		return this.pieceMoveVal;
	}
}
