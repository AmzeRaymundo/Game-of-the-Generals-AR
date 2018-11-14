using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {
	[SerializeField] private Camera arCamera;
	[SerializeField] private GameObject[] tilesGameObject;

	// Use this for initialization
	void Start () {
		Board.Instance.setTiles (this.tilesGameObject);
		Board.Instance.setBoardGameObject (this.gameObject);
		print ("Board is set");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
