using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building {
	public class BuildingController : MonoBehaviour {
		private BuildingStatus status;
		private int playerId;

		[SerializeField] private SpriteRenderer spriteRenderer;

		public int PlayerId { get { return playerId; } }

		public BuildingStatus Status { get { return status; } }
		
		void Start () {

		}

		void Update () {

		}

		public void SetData(int _playerId, BuildingStatus _status, Sprite _sprite) {
			this.playerId = _playerId;
			this.status = _status;
			this.spriteRenderer.sprite = _sprite;
		}
	}
}

