using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Colonize.DefaultManager;

namespace Colonize.ControllUI.UnitControll {
	public sealed class BuildButtonRoll : ButtonRoll {
		private static float buttonWidth;
		private static float outerImageOriginLocalPosX;

		private bool active;
		private Unit.Building.BuildingType buildingType;
		private Coroutine coroutine;

		public int cost { get; set;}

		internal BuildButtonRoll(Unit.Building.BuildingType _buildingType) {
			this.buildingType = _buildingType;
		}
        
		internal override void SetButtonForWake(UnitControllButton _button) {
			if(buttonWidth == 0.0f) {
				buttonWidth = (_button.transform as RectTransform).sizeDelta.x;
				outerImageOriginLocalPosX = _button.OuterImage.transform.localPosition.x;
			}
			switch(Communicate.CommunicateManager.Instance.PlayerId) {
				case 0:
					_button.UnitImage.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", "BP_Commander");
					break;
				case 1:
					_button.UnitImage.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", "WP_Commander");
					break;
				default:
					_button.UnitImage.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", "BP_Commander");
					break;
			}
			
			_button.UnitImage.gameObject.SetActive(true);
			_button.UnitCountText.gameObject.SetActive(true);
		}
        
		internal override void Click(UnitControllButton _button) {
			if(active) {
				_button.ImageMask.gameObject.SetActive(true);
				float boardClickPosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - _button.transform.position.x;
				_button.ImageMask.transform.localPosition = new Vector2(-buttonWidth * 0.25f, 0.0f);
				_button.MyControllBoard.SetControll(_button.MyControllBoard.ClickOnBuildOption(), this.buildingType, this.cost);
			} else {
				this.active = true;
				_button.ImageMask.gameObject.SetActive(false);
				_button.MyControllBoard.SetControll(_button.MyControllBoard.ClickNoOption(), Unit.Piece.PieceType.End);
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
				yield return new WaitForSecondsRealtime(0.01f);
				_button.OuterImage.transform.Translate(-5.0f, 0.0f, 0.0f);
			}
			_button.OuterImage.transform.localPosition = new Vector3(outerImageOriginLocalPosX - buttonWidth, 0.0f, 0.0f);
			this.coroutine = null;
		}

		private IEnumerator MoveImageRight(UnitControllButton _button) {
			while(outerImageOriginLocalPosX >= _button.OuterImage.transform.localPosition.x) {
				yield return new WaitForSecondsRealtime(0.01f);
				_button.OuterImage.transform.Translate(5.0f, 0.0f, 0.0f);
			}
			_button.OuterImage.transform.localPosition = new Vector3(outerImageOriginLocalPosX, 0.0f, 0.0f);
			this.coroutine = null;
		}
	}
}