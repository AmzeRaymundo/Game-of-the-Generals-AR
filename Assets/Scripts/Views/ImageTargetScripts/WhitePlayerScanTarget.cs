using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class WhitePlayerScanTarget :  ImageTargetBehaviour{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnTrackerUpdate(Status newStatus){
		base.OnTrackerUpdate (newStatus);

		if (newStatus == Status.TRACKED) {
			if (!GameSceneHandler.Instance.getWhiteInitialized())
				EventBroadcaster.Instance.PostEvent (EventNames.ON_WHITE_LOGO_SCAN);
			else
				EventBroadcaster.Instance.PostEvent (EventNames.ON_WHITE_TURN_ACTIVATE);
		}

		else if (newStatus == Status.NOT_FOUND) {
			EventBroadcaster.Instance.PostEvent (EventNames.ON_RETURN_TO_DEFAULT_MSG);
		}
	}
}
