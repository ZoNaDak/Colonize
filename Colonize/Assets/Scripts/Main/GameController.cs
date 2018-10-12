using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.DefaultManager {
	public class GameController : Pattern.Singleton.MonoSingleton<GameController> {
		private int playerID;
		private int playerNum;

		[SerializeField] private Communicate.CommunicateManager communicator;
		[SerializeField] private Unit.Building.BuildingManager buildingManager;
		[SerializeField] private Unit.Piece.PieceManager pieceManager;
		[SerializeField] private MyCamera.MainCameraController mainCamera;

		public int PlayerId { get { return playerID; } }
		public int PlayerNum { get { return playerNum; } }

		void Awake() {
			#if !UNITY_EDITOR
				Debug.unityLogger.logEnabled = false;
			#endif

			//SetResoultion
			//Screen.SetResolution(720, 1280, false);
			Screen.SetResolution(360, 640, false);

			//임시
			this.playerID = 1;
			this.playerNum = 2;
		}

		// Use this for initialization
		void Start () {
			//SetAstar
			Utility.Algorithm.AStar.AStarManager.Awake(Map.MapManager.Instance.LandNumX, Map.MapManager.Instance.LandNumY);
			//
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
			this.mainCamera.SetPos(landPos);
			this.buildingManager.CreateUnit(Unit.Building.BuildingType.Commander, landPos);
		}

		// Update is called once per frame
		void Update () {

		}
	}
}

