using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

namespace UnitControll {
	public enum ButtonType {
		Camera,
		End
	}

	public class UnitControllButton : MonoBehaviour {
		private static UnitControllBar unitControllBar;
		private static RedRect redRect;
		private static ControllBoard.ControllBoard controllBoard;
		private ButtonType buttonType = ButtonType.End;

		[SerializeField] private Image unitImage;
		[SerializeField] private Text unitCount;

		public ButtonType ButtonType { get { return buttonType; } }

		void Awake() {
			this.unitImage.gameObject.SetActive(false);
			this.unitCount.gameObject.SetActive(false);
		}
		
		internal void Wake(ButtonType _type) {
			if(unitControllBar == null) {
				unitControllBar = UnitControllBar.Instance;
				redRect = unitControllBar.RedRect;
				controllBoard = ControllBoard.ControllBoard.Instance;
			}
			this.buttonType = _type;

			switch(_type) {
				case ButtonType.Camera:
					this.unitImage.sprite = SpirteFactory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Eye");
					this.unitImage.transform.position = this.transform.position;
					this.unitImage.gameObject.SetActive(true);
					redRect.SetControll(this);
					controllBoard.SetControll(controllBoard.ClickOnCameraOption());
				break;
				default:
					throw new System.ArgumentException(string.Format("{0} is not UnitControllButtonType!", _type.ToString()), "_type");
			}
		}

		internal void Click() {
			if(this.buttonType == ButtonType.End) {
				return;
			}

			try {
				redRect.SetControll(this);
				switch(this.buttonType) {
					case ButtonType.Camera:
						controllBoard.SetControll(controllBoard.ClickOnCameraOption());
					break;
					default:
						throw new System.ArgumentException("ButtonType is Not Correct!");
				}
			} catch(System.ArgumentException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}
	}
}