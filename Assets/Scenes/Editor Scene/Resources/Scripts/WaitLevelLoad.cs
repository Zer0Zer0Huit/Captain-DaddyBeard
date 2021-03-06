﻿using UnityEngine;
using System.Collections;

/*
 * This script is used when :
 * - a phase is finished : the 'PhaseArrival' script will then call an RPC below to tell Android to load the next scene.
 * - the player collide with a dead zone : the 'DeadZone' script will do the same than above.
 * 
 * Both Windows and Android (although it does nothing on Windows).
 * Use this script only for the prefabs of phases.
 */
public class WaitLevelLoad : MonoBehaviour {
	
	#if UNITY_ANDROID
	
	private void Start () {
		// Server tells clients when to load next phase.
		RPCWrapper.RegisterMethod (LoadNextPhase);
		RPCWrapper.RegisterMethod (LoadScoreScene);
		RPCWrapper.RegisterMethod (ReloadCurrentPhase);
		RPCWrapper.RegisterMethod (ReloadLevel);
	}
	
	private void LoadNextPhase (int nextPhaseType, int nextLevel, int nextPhase) {
		PhaseLoader.Load (nextLevel, nextPhase, (PhaseLoader.Type) nextPhaseType);
	}
	
	private void LoadScoreScene () {
		if (Player.id.Get () == 1)
			Application.LoadLevel ("Android - ScoreScene1");
		else if (Player.id.Get () == 2)
			Application.LoadLevel ("Android - ScoreScene2");
	}

	private void ReloadCurrentPhase () {
		PhaseLoader.ReloadPhase ();
	}

	private void ReloadLevel () {
		PhaseLoader.Reload ();
	}

	#endif
}
