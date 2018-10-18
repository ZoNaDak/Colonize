using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

namespace Colonize.DefaultManager {
	public class GameController : MonoBehaviour {
		private int playerID;
		private int playerNum;
		private Communicate.CommunicateManager communicator;

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
			
			communicator = GameObject.FindGameObjectWithTag("Communicator").GetComponentInChildren<Communicate.CommunicateManager>();

			this.playerID = communicator.PlayerId;
			this.playerNum = 2;

			//Setting Serialization Of CustomType
			PhotonPeer.RegisterType(typeof(Unit.Building.BuildingStatus), (byte)100, Unit.Building.BuildingStatus.Serialize, Unit.Building.BuildingStatus.Deserialize);
			PhotonPeer.RegisterType(typeof(Unit.Piece.PieceStatus), (byte)101, Unit.Piece.PieceStatus.Serialize, Unit.Piece.PieceStatus.Deserialize);
		}

		// Use this for initialization
		void Start () {
			StartCoroutine(Initialize());
		}

		// Update is called once per frame
		void Update () {

		}

		private IEnumerator Initialize() {
			yield return new WaitUntil(() => PhotonNetwork.connectionStateDetailed == ClientState.Joined);
			yield return new WaitForSecondsRealtime(3.0f);
			//SetAStar
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
	}
}

