using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTileBehaviour : MonoBehaviour {
	[SerializeField] private Camera arCamera;
	private Piece piece;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = this.arCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
		}
		
	}
}
