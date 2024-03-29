﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

namespace Communicate {
	public class CommunicateManager : Pattern.Singleton.PhotonMonoSingleton<CommunicateManager> {
		private int playerID;
		private bool joinedRoom;
		private bool gameWin;
		private bool gameResult;

		public int PlayerId { get { return playerID; } }
		public bool JoinedRoom { get { return joinedRoom; } set { joinedRoom = value; } }
		public bool GameWin { get { return gameWin; } set { gameWin = value; } }
		public bool GameResult { get { return gameResult; } set { gameResult = value; } }

		void Awake() {
			PhotonNetwork.ConnectUsingSettings("0.1");
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.automaticallySyncScene = true;
			PhotonNetwork.autoCleanUpPlayerObjects = true;
		}

		void Start () {
			JoinLobby();
		}

		void Update () {
			
		}

		void OnGUI() {
			#if UNITY_EDITOR
				GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
			#endif
		}
		
		public bool JoinLobby() {
			return PhotonNetwork.JoinLobby();
		}

		public void CreateRoom(RoomOptions _roomOption) {
			if(PhotonNetwork.JoinLobby()) {
				playerID = 0;
				PhotonNetwork.CreateRoom(string.Format("Room {0}", PhotonNetwork.GetRoomList().Length), _roomOption, TypedLobby.Default);
				Debug.Log("Created Room");
			} else {
				CreateRoom(_roomOption);
			}
		}

		public void JoinRoom(string _roomName) {
			if(PhotonNetwork.JoinLobby()) {
				playerID = 1;
				PhotonNetwork.JoinRoom(_roomName);
				Debug.Log("Joined Room");
			} else {
				JoinRoom(_roomName);
			}
		}

		public void LeaveRoom() {
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.LeaveLobby();
		}

		public RoomInfo[] GetRooms() {
			return PhotonNetwork.GetRoomList();
		}

		public bool CheckPlayer() {
			if(PhotonNetwork.otherPlayers.Length > 0) {
				return true;
			}
			return false;
		}

		//Photon
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			
		}

		void OnPhotonCreateRoomFaild(object[] codeAndMsg) {
			Debug.Log("Failed" + codeAndMsg[1]);
		}
	}
}