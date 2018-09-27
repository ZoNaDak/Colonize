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
			this.rectTransform = (this.transform as RectTransform);
		}

		public void SetControll (UnitControllButton _button) {
			this.transform.position = _button.transform.position;
			this.buttonType = _button.ButtonType;
			if(this.buttonType == ButtonType.MoveAndAttack) {
				if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - _button.transform.position.x <= 0) {
					this.transform.Translate(new Vector2(this.ButtonSize.x * -0.25f , 0.0f));
					this.buttonType = ButtonType.Move;
				} else {
					this.transform.Translate(new Vector2(this.ButtonSize.x * 0.25f , 0.0f));
					this.buttonType = ButtonType.Attack;
				}
				this.rectTransform.sizeDelta = new Vector2(this.ButtonSize.x * 0.5f, this.ButtonSize.y);
			} else {
				this.rectTransform.sizeDelta = new Vector2(this.ButtonSize.x, this.ButtonSize.y);
			}
		}
	}
}
