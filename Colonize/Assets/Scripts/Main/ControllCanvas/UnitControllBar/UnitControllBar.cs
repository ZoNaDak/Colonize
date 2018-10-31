using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.ControllUI.UnitControll {
	public class UnitControllBar : Pattern.Singleton.MonoSingleton<UnitControllBar> {
		private List<UnitControllButton> unitControllButtonList = new List<UnitControllButton>(8);
		private const int buttonNum = 8;
		private bool awaked;

		[SerializeField] private GameObject buttons;
		[SerializeField] private RedRect redRect;

		public bool Awaked { get { return awaked; } }
		public RedRect RedRect { get { return redRect; } }

		void Awake() {
			GameObject unitControllButtonPrefab = Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Controll", "UnitControllButton", false);
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
			} finally {
				awaked = true;
			}
			//InnerImage
			Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Controll", "InnerImageForPieceButton", true);
			Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Controll", "InnerImageForBuildButton", true);
		}

		void Start() {
			WakeButtons();
		}

		void Update () {

		}

		private void WakeButtons() {
			UnitControllButton.InitButton();
			this.unitControllButtonList[0].WakeCamera();
			this.unitControllButtonList[1].WakePiece(Unit.Piece.PieceType.SwordMan);
			this.unitControllButtonList[2].WakePiece(Unit.Piece.PieceType.Archer);
			this.unitControllButtonList[4].WakeBuild(Unit.Building.BuildingType.Commander);
			this.unitControllButtonList[5].WakeBuild(Unit.Building.BuildingType.Mine);
			this.unitControllButtonList[6].WakeBuild(Unit.Building.BuildingType.ArcheryField);
		}

		public UnitControllButton FindButton(Unit.Piece.PieceType _type) {
			for(int i = 0; i < unitControllButtonList.Count; ++i) {
				if(unitControllButtonList[i].ButtonType == ButtonType.Piece) {
					if((unitControllButtonList[i].ButtonRoll as PieceButtonRoll).PieceType == _type) {
						return unitControllButtonList[i];
					}
				}
			}

			throw new System.ArgumentNullException(string.Format("Can't Find Button. Type : {0}" + _type));
		}

		public UnitControllButton FindButton(Unit.Building.BuildingType _type) {
			for(int i = 0; i < unitControllButtonList.Count; ++i) {
				if(unitControllButtonList[i].ButtonType == ButtonType.Build) {
					if((unitControllButtonList[i].ButtonRoll as BuildButtonRoll).BuildingType == _type) {
						return unitControllButtonList[i];
					}
				}
			}

			throw new System.ArgumentNullException(string.Format("Can't Find Button. Type : {0}" + _type));
		}

		public IEnumerator WaitForReady() {
			while(!awaked) {
				yield return false;
			}
			yield return true;
		}
	}
}

