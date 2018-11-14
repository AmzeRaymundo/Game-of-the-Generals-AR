using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SurrenderScanTarget : ImageTargetBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnTrackerUpdate(Status newStatus){
		base.OnTrackerUpdate (newStatus);

		if (newStatus == Status.TRACKED) {
			EventBroadcaster.Instance.PostEvent (EventNames.ON_SCAN_SURRENDER);
		}
	}
}
