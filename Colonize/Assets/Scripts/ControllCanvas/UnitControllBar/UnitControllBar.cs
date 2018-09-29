using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitControll {
	public class UnitControllBar : SingletonPattern.MonoSingleton<UnitControllBar> {
		private List<UnitControllButton> unitControllButtonList = new List<UnitControllButton>(8);
		private const int buttonNum = 8;

		[SerializeField] private GameObject buttons;
		[SerializeField] private RedRect redRect;

		public RedRect RedRect { get { return redRect; } }

		void Awake() {
			GameObject unitControllButtonPrefab = Prefab.PrefabFactory.Instance.CreatePrefab("Controll", "UnitControllButton", false);
			try {
				float buttonSize = unitControllButtonPrefab.GetComponent<RectTransform>().rect.height;
				float startPosY =  (this.GetComponent<RectTransform>().rect.height - buttonSize) * 0.5f;
				for(int i = 0; i < buttonNum; ++i) {
					UnitControllButton button = Instantiate(unitControllButtonPrefab, this.buttons.transform).GetComponent<UnitControllButton>();
					button.transform.localPosition = new Vector3(0.0f, startPosY - buttonSize * i , 0.0f);
					this.unitControllButtonList.Add(button);
				}
				this.redRect.ButtonSize = (this.unitControllButtonList[0].transform as RectTransform).sizeDelta;
			} catch (System.NullReferenceException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
		}

		void Start() {
			this.unitControllButtonList[0].Wake(UnitControll.ButtonType.Camera);
		}

		void Update () {

		}
	}
}

