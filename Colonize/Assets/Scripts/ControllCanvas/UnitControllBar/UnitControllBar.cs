using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitControll {
	public class UnitControllBar : MonoBehaviour {
		private List<UnitControllButton> unitControllButtonList = new List<UnitControllButton>(8);
		private const int buttonNum = 8;

		void Awake() {
			GameObject unitControllButtonPrefab = Prefab.PrefabFactory.Instance.CreatePrefab("Controll", "UnitControllButton", false);	
			if(unitControllButtonPrefab != null) {
				float buttonSize = unitControllButtonPrefab.GetComponent<RectTransform>().rect.height;
				float startPosY =  this.transform.position.y + this.GetComponent<RectTransform>().rect.height * 0.5f - buttonSize * 0.5f;
				for(int i = 0; i < buttonNum; ++i) {
					UnitControllButton button = Instantiate(unitControllButtonPrefab
						, new Vector3(this.transform.position.x, startPosY - buttonSize * i , this.transform.position.z)
						, Quaternion.identity
						, this.transform).GetComponentInChildren<UnitControllButton>();
					this.unitControllButtonList.Add(button);
				}
			}
		}

		void Start () {

		}

		void Update () {

		}
	}
}

