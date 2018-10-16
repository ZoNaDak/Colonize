using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

namespace Communicate {
	public class CommunicateManager : Pattern.Singleton.PhotonMonoSingleton<CommunicateManager> {
		private int playerID;

		public int PlayerId { get { return playerID; } }

		void Awake() {
			PhotonNetwork.ConnectUsingSettings("0.1");
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
				PhotonNetwork.CreateRoom(string.Format("Room {0}", PhotonNetwork.GetRoomList().Length), _roomOption, TypedLobby.Default);
				playerID = 0;
				Debug.Log("Created Room");
			} else {
				CreateRoom(_roomOption);
			}
		}

		public void JoinRoom(string _roomName) {
			if(PhotonNetwork.JoinLobby()) {
				PhotonNetwork.JoinRoom(_roomName);
				playerID = 1;
				Debug.Log("Joined Room");
			} else {
				JoinRoom(_roomName);
			}
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
	}
}