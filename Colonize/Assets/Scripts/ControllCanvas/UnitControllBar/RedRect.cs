using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitControll {
	public class RedRect : MonoBehaviour {
		private ButtonType buttonType;
		
		public Vector2 ButtonSize { get; set; }

		public ButtonType ButtonType { get { return buttonType; } }

		public void SetControll (UnitControllButton _button) {
			this.transform.position = _button.transform.position;
			this.buttonType = _button.ButtonType;
		}
	}
}
