using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using Colonize;

namespace Colonize.ControllUI.UnitControll {
	public enum ButtonType {
		Camera,
		SwordMan,
		End
	}

	public class UnitControllButton : MonoBehaviour, Pattern.Observer.IObserverOfUnit<Unit.UnitController<Unit.Piece.PieceController, Unit.Piece.PieceStatus, Unit.Piece.PieceType>> {
		private static UnitControllBar unitControllBar;
		private static RedRect redRect;
		private static ControllBoard.ControllBoard controllBoard;

		private ButtonType buttonType = ButtonType.End;
		private ButtonRoll buttonRoll;

		[SerializeField] private Image image;
		[SerializeField] private Image unitImage;
		[SerializeField] private Text unitCountText;
		[SerializeField] private Image imageMask;
		[SerializeField] private GameObject outerImage;

		public ButtonType ButtonType { get { return buttonType; } }

		internal Image MyImage { get { return image; } }
		internal Image UnitImage { get { return unitImage; } }
		internal Text UnitCountText { get { return unitCountText; } }
		internal Image ImageMask { get { return imageMask; } }
		internal GameObject OuterImage { get { return outerImage; } }
		internal ControllBoard.ControllBoard MyControllBoard { get { return controllBoard; } }

		void Awake() {
			this.unitImage.gameObject.SetActive(false);
			this.unitCountText.gameObject.SetActive(false);
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
					buttonRoll = new CameraButtonRoll();
					redRect.SetControll(this);
				break;
				case ButtonType.SwordMan:
					buttonRoll = new PieceButtonRoll(Unit.Piece.PieceType.SwordMan);
				break;
				default:
					throw new System.ArgumentException(string.Format("{0} is not UnitControllButtonType!", _type.ToString()), "_type");
			}
			buttonRoll.SetButtonForWake(this);
		}

		internal void Click() {
			if(this.buttonType == ButtonType.End) {
				return;
			}

			try {
				if(redRect.ActiveButton.ButtonType != this.ButtonType) {
					redRect.ActiveButton.Deactive();
					redRect.SetControll(this);
				}
				buttonRoll.Click(this);
			} catch(System.ArgumentException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		internal void Deactive() {
			buttonRoll.DeactiveButton(this);
		}

		//Interface Fuction
		public void OnNotify(Unit.UnitController<Unit.Piece.PieceController, Unit.Piece.PieceStatus, Unit.Piece.PieceType> _piece) {
			unitCountText.text = _piece.UnitNum.ToString();
		}
	}
}