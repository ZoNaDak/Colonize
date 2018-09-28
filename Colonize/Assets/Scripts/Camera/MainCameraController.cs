using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCamera {
	public class MainCameraController : MonoBehaviour {
		private float controllUIHalfSizeY;

		[SerializeField] private ControllBoard.ControllBoard controllBoard;

		void Start () {
			controllUIHalfSizeY = (controllBoard.transform as RectTransform).sizeDelta.y * 0.5f;
		}

		public void SetPos(Vector2 _pos) {
			this.transform.position = new Vector3(_pos.x, _pos.y, this.transform.position.z);
			this.transform.Translate(0.0f, -controllUIHalfSizeY, 0.0f);
		}

		public Vector2 GetPos() {
			return new Vector2(this.transform.position.x, this.transform.position.y + controllUIHalfSizeY);
		}
	}
}
