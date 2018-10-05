using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitControll {
	public class CameraButtonRoll : ButtonRoll {
		internal override void SetButtonForWake(UnitControllButton _button) {
			_button.UnitImage.sprite = SpirteFactory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Eye");
			_button.UnitImage.transform.position = _button.transform.position;
			_button.UnitImage.gameObject.SetActive(true);
			_button.MyControllBoard.SetControll(_button.MyControllBoard.ClickOnCameraOption());
		}

		internal override void Click(UnitControllButton _button) {
			_button.MyControllBoard.SetControll(_button.MyControllBoard.ClickOnCameraOption());
		}

		internal override void DeactiveButton(UnitControllButton _button) {}
	}
}

