using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllBoard {
	public class YellowRect : MonoBehaviour {
		private Vector2 landSize;
		private RectTransform rectTransform;

		public Vector2 LandSize { set { landSize = value; } }

		void Awake() {
			this.rectTransform = this.transform as RectTransform;
		}

		public void SetPos(Vector3 _cameraPos) {
			this.rectTransform.anchoredPosition = new Vector3(
				(int)(_cameraPos.x / landSize.x) * this.rectTransform.sizeDelta.x,
				(int)(_cameraPos.y / landSize.y) * this.rectTransform.sizeDelta.y);
		}
	}
}
