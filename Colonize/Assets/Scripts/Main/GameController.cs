﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Pattern.Factory;
using Colonize.Player;
using Colonize.Utility.Coroutine;

namespace Colonize.DefaultManager {
	public class GameController : Pattern.Singleton.MonoSingleton<GameController> {
		private bool ready;
		private bool gameStart;
		private int playerID;
		private int playerNum;
		private Communicate.CommunicateManager communicator;
		private List<PlayerController> playerList = new List<PlayerController>();
		private PlayerController myPlayer;
		private Coroutine notifyTextCoroutine;

		[SerializeField] private MyCamera.MainCameraController mainCamera;
		[SerializeField] private Text goldText;
		[SerializeField] private Text notifyText;

		public bool Ready { get { return ready; } }
		public int PlayerId { get { return playerID; } }
		public int PlayerNum { get { return playerNum; } }
		public PlayerController MyPlayer { get { return myPlayer; } }

		void Awake() {
			#if !UNITY_EDITOR
				Debug.unityLogger.logEnabled = false;
			#endif

			//SetResoultion
			//Screen.SetResolution(720, 1280, false);
			Screen.SetResolution(360, 640, false);
			
			communicator = Communicate.CommunicateManager.Instance;

			PhotonNetwork.isMessageQueueRunning = true;
			gameStart = false;

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
			if(this.gameStart) {
				if(this.myPlayer.CheckLose()) {
					this.communicator.GameWin = false;
					StartCoroutine(LoadTitleScene());
				} else if (!this.communicator.CheckPlayer()) {
					this.communicator.GameWin = true;
					StartCoroutine(LoadTitleScene());
				}
			} 
		}

		private IEnumerator Initialize() {
			yield return new WaitUntil(() => PhotonNetwork.connectionStateDetailed == ClientState.Joined);
			this.playerID = communicator.PlayerId;
			this.playerNum = 2;
			
			//CreatePlayer
			for(int i = 0; i < playerNum; ++i) {
				GameObject playerPrefab = PrefabFactory.Instance.CreatePrefab("Player", "Player", false);
				PlayerController player = Instantiate(playerPrefab, this.transform.position, Quaternion.identity).GetComponentInChildren<PlayerController>();
				player.Intialize(i);
				playerList.Add(player);
			}
			this.myPlayer = this.playerList[this.playerID];
			this.myPlayer.goldText = this.goldText;

			this.ready = true;

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
			this.myPlayer.CreateForGameStart(landPos);

			this.gameStart = true;
		}

		public PlayerController GetPlayer(int _playerId) {
			return playerList[_playerId];
		}

		public void SetNotifyText(string _text) {
			if(notifyTextCoroutine != null) {
				StopCoroutine(notifyTextCoroutine);
			}
			this.notifyText.gameObject.SetActive(true);
			this.notifyText.text = _text;
			notifyTextCoroutine = StartCoroutine(UseableCoroutine.WaitThenCallback(5.0f, () => this.notifyText.gameObject.SetActive(false), this.notifyTextCoroutine));
		}

		//Coroutine
		IEnumerator LoadTitleScene() {
			this.communicator.GameResult = true;
			this.communicator.LeaveRoom();
			PhotonNetwork.isMessageQueueRunning = false;
			Pattern.Factory.PrefabFactory.Instance.AllClearDictionary();
			SceneManager.LoadScene("Title");
 			
 			yield return null;
		}
	}
}

