using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitControll {
	public class PieceButtonRoll : ButtonRoll {
		private static float buttonWidth;
		private static float outerImageOriginLocalPosX;

		private bool active;
		private Coroutine coroutine;

		internal override void SetButtonForWake(UnitControllButton _button) {
			if(buttonWidth == 0.0f) {
				buttonWidth = (_button.transform as RectTransform).sizeDelta.x;
				outerImageOriginLocalPosX = _button.OuterImage.transform.localPosition.x;
			}
			_button.UnitImage.sprite = SpirteFactory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Sword");
			_button.UnitImage.gameObject.SetActive(true);
			_button.UnitCountText.gameObject.SetActive(true);
		}

		internal override void Click(UnitControllButton _button) {
			if(active) {
				_button.ImageMask.gameObject.SetActive(true);
				float boardClickPosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - _button.transform.position.x;
				if(boardClickPosX < 0.0f) {
					_button.ImageMask.transform.localPosition = new Vector2(-buttonWidth * 0.25f, 0.0f);
					_button.MyControllBoard.SetControll(_button.MyControllBoard.ClickOnMoveOption());
				} else {
					_button.ImageMask.transform.localPosition = new Vector2(buttonWidth * 0.25f, 0.0f);
					_button.MyControllBoard.SetControll(_button.MyControllBoard.ClickOnAttackOption());
				}
			} else {
				this.active = true;
				_button.ImageMask.gameObject.SetActive(false);
				_button.MyControllBoard.SetControll(_button.MyControllBoard.ClickNoOption());
				if(this.coroutine != null) {
					_button.StopCoroutine(this.coroutine);
				}
				this.coroutine = _button.StartCoroutine(this.MoveImageLeft(_button));
			}
		}

		internal override void DeactiveButton(UnitControllButton _button) {
			this.active = false;
			if(this.coroutine != null) {
				_button.StopCoroutine(this.coroutine);
			}
			this.coroutine = _button.StartCoroutine(this.MoveImageRight(_button));
		}

		//Coroutine
		private IEnumerator MoveImageLeft(UnitControllButton _button) {
			while(outerImageOriginLocalPosX - buttonWidth < _button.OuterImage.transform.localPosition.x) {
				yield return new WaitForSeconds(0.01f);
				_button.OuterImage.transform.Translate(-5.0f, 0.0f, 0.0f);
			}
			_button.OuterImage.transform.localPosition = new Vector3(outerImageOriginLocalPosX - buttonWidth, 0.0f, 0.0f);
			this.coroutine = null;
		}

		private IEnumerator MoveImageRight(UnitControllButton _button) {
			while(outerImageOriginLocalPosX >= _button.OuterImage.transform.localPosition.x) {
				yield return new WaitForSeconds(0.01f);
				_button.OuterImage.transform.Translate(5.0f, 0.0f, 0.0f);
			}
			_button.OuterImage.transform.localPosition = new Vector3(outerImageOriginLocalPosX, 0.0f, 0.0f);
			this.coroutine = null;
		}
	}
}