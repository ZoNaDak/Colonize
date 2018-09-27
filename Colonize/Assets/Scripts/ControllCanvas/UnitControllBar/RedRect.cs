using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitControll {
	public class RedRect : MonoBehaviour {
		private ButtonType buttonType;
		private RectTransform rectTransform;
		
		public Vector2 ButtonSize { get; set; }

		public ButtonType ButtonType { get { return buttonType; } }

		public void Awake() {
			rectTransform = (this.transform as RectTransform);
		}

		public void SetControll (UnitControllButton _button) {
			this.transform.position = _button.transform.position;
			buttonType = _button.ButtonType;
			if(buttonType == ButtonType.MoveAndAttack) {
				if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - _button.transform.position.x <= 0) {
					this.transform.Translate(new Vector2(ButtonSize.x * -0.25f , 0.0f));
				} else {
					this.transform.Translate(new Vector2(ButtonSize.x * 0.25f , 0.0f));
				}
				rectTransform.sizeDelta = new Vector2(ButtonSize.x * 0.5f, ButtonSize.y);
			} else {
				rectTransform.sizeDelta = new Vector2(ButtonSize.x, ButtonSize.y);
			}
		}
	}
}
