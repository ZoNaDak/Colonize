using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultManager {
	public class GameController : SingletonPattern.MonoSingleton<GameController> {
		private int playerID;
		private int playerNum;

		[SerializeField] private Communicate.CommunicateManager communicator;
		[SerializeField] private MyCamera.MainCameraController mainCamera;

		void Awake() {
			#if !UNITY_EDITOR
				Debug.unityLogger.logEnabled = false;
			#endif
			Screen.SetResolution(720, 1280, false);
			//임시
			this.playerID = 0;
			this.playerNum = 2;
		}

		// Use this for initialization
		void Start () {
			SetCamera();
		}

		// Update is called once per frame
		void Update () {

		}

		private void SetCamera() {
			Vector2 landPos;
			switch(this.playerNum) {
				case 2:
					if(this.playerID == 0) {
						landPos = Map.MapManager.Instance.GetLandPos(4, 0);
					} else {
						landPos = Map.MapManager.Instance.GetLandPos(0, 4);
					}
				break;
				default:
					throw new System.ArgumentException("Player Number is Not Correct!");
			}
			this.mainCamera.SetPos(new Vector2(landPos.x, landPos.y));
		}
	}
}

