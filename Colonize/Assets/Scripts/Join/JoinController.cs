using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Colonize.Join {
	public class JoinController : Pattern.Singleton.MonoSingleton<JoinController> {
		
		private List<Room.RoomController> roomList = new List<Room.RoomController>();
		private int waitTextPeriodNum;
		private Coroutine wait;
		private Communicate.CommunicateManager communicator;

		[SerializeField] private GameObject rooms;
		[SerializeField] private GameObject roomPrefab;
		[SerializeField] private GameObject refreshButton;
		[SerializeField] private GameObject createRoomButton;
		[SerializeField] private Text waitText;

		void Awake() {
			Screen.SetResolution(360, 640, false);

			this.communicator = Communicate.CommunicateManager.Instance;
			DontDestroyOnLoad(this.communicator.gameObject);
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		private void ClearRoomList() {
			if (this.roomList.Count > 0) {
				for (int i = 0; i < this.roomList.Count; i++) {
					Destroy(this.roomList[i]);
				}
				this.roomList.Clear ();
			}
		}

		private IEnumerator OnWait() {
			while(true) {
				if(communicator.CheckPlayer()) {
					PhotonNetwork.room.IsOpen = false;
					communicator.JoinedRoom = true;
					StartCoroutine(LoadMainScene());
				}
				waitText.text = "Wait";
				for(int i = 0; i < waitTextPeriodNum; ++i) {
					waitText.text += ".";
				}
				waitTextPeriodNum++;
				if(waitTextPeriodNum >= 5) {
					waitTextPeriodNum = 0;
				}
				yield return new WaitForSecondsRealtime(0.5f);
			}
		}

		public void JoinRoom(Room.RoomController _room) {
			communicator.JoinRoom(_room.RoomName);
			ClearRoomList();
			communicator.JoinedRoom = true;
		}

		//Button
		public void OnClickCreateRoomButton() {
			RoomOptions roomOption = new RoomOptions();
			roomOption.MaxPlayers = 2;
			roomOption.IsVisible = true;
			roomOption.IsOpen = true;
			this.communicator.CreateRoom(roomOption);
			ClearRoomList();
			this.refreshButton.SetActive(false);
			this.createRoomButton.SetActive(false);
			this.waitText.gameObject.SetActive(true);
			StartCoroutine(OnWait());
		}

		public void OnClickRefreshButton() {
			if(!communicator.JoinLobby()) {
				OnClickRefreshButton();
				return;
			}

			ClearRoomList();

			float startYPos = 200.0f;
			float roomPosInterval = (roomPrefab.transform as RectTransform).sizeDelta.y + 20.0f;
			RoomInfo[] roomInfoes = this.communicator.GetRooms();
			for (int i = 0; i < roomInfoes.Length; i++) {
				if(roomInfoes[i].IsOpen == false){
					continue;
				}
				Room.RoomController room = Instantiate(this.roomPrefab).GetComponent<Room.RoomController>();
				room.transform.SetParent (this.rooms.transform);
				room.transform.localPosition = new Vector3(0.0f, startYPos -roomPosInterval * i);
				room.transform.localScale = this.roomPrefab.transform.localScale;
				room.Initialize(roomInfoes[i].Name);
				this.roomList.Add(room);
			}
		}

		//Coroutine
		IEnumerator LoadMainScene() {
			PhotonNetwork.isMessageQueueRunning = false;

			AsyncOperation ao = PhotonNetwork.LoadLevelAsync("Main");
 			
 			yield return ao;
		}
	}
}

