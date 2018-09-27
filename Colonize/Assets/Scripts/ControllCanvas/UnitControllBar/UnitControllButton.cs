using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

namespace UnitControll {
	public enum ButtonType {
		Camera,
		MoveAndAttack,
		End
	}

	public class UnitControllButton : MonoBehaviour {
		private UnitControllBar unitControllBar;
		private ButtonType buttonType = ButtonType.End;
		private RedRect redRect;

		[SerializeField] private Image unitImage;
		[SerializeField] private Text unitCount;

		public ButtonType ButtonType { get { return buttonType; } }

		void Awake() {
			unitImage.gameObject.SetActive(false);
			unitCount.gameObject.SetActive(false);
		}
		
		public void Wake(ButtonType _type) {
			this.unitControllBar = UnitControllBar.Instance;
			this.redRect = unitControllBar.RedRect;
			buttonType = _type;

			switch(_type) {
				case ButtonType.Camera:
					unitImage.sprite = SpirteFactory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Eye");
					unitImage.transform.position = this.transform.position;
					unitImage.gameObject.SetActive(true);
					redRect.SetControll(this);
				break;
				case ButtonType.MoveAndAttack:
					unitImage.sprite = SpirteFactory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Move&Attack");
					unitImage.transform.position = this.transform.position;
					(unitImage.transform as RectTransform).sizeDelta = new Vector2(220.0f, 62.5f);
					unitImage.gameObject.SetActive(true);
				break;
				default:
					throw new System.ArgumentException(string.Format("{0} is not UnitControllButtonType!", _type.ToString()), "_type");
			}
		}

		public void Click() {
			if(buttonType == ButtonType.End) {
				return;
			} 
			redRect.SetControll(this);
		}
	}
}