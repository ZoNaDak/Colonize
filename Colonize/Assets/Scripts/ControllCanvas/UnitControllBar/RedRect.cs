using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitControll {
	public class RedRect : MonoBehaviour {
		private UnitControllButton activeButton;
		
		public Vector2 ButtonSize { get; set; }

		public UnitControllButton ActiveButton { get { return activeButton; } }

		public void SetControll (UnitControllButton _button) {
			this.transform.position = _button.transform.position;
			this.activeButton = _button;
		}
	}
}
