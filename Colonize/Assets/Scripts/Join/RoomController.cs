using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Colonize.Join.Room {
	public class RoomController : MonoBehaviour {
		[SerializeField] private Text roomName;

		public string RoomName { get { return roomName.text; } }

		void Start () {

		}

		void Update () {

		}

		public void Initialize(string _roomName) {
			this.roomName.text = _roomName;
		}

		public void OnClickEnterRoom() {
			Join.JoinController.Instance.JoinRoom(this);
		}
	}
}

