using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitControll {
	public abstract class ButtonRoll {
		internal abstract void SetButtonForWake(UnitControllButton _button);
		internal abstract void Click(UnitControllButton _button);
		internal abstract void DeactiveButton(UnitControllButton _button);
	}
}

