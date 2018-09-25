using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultManager {
	public class GameController : SingletonPattern.MonoSingleton<GameController> {
		[SerializeField] private Communicate.CommunicateManager communicator;
		void Awake() {
			#if !UNITY_EDITOR
				Debug.unityLogger.logEnabled = false;
			#endif
			Screen.SetResolution(720, 1280, false);
		}

		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {

		}
	}
}

