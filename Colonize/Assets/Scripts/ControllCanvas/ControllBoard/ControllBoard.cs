using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllBoard {
	public class ControllBoard : SingletonPattern.MonoSingleton<ControllBoard> {
		public delegate void ClickDelegate();

		private RectTransform rectTransform;
		private Vector2 blockSize = new Vector2();
		private float clickedTime;
		private bool drag = false;

		private System.Action OnClick;

		[SerializeField] private YellowRect yellowRect;
		[SerializeField] private MyCamera.MainCameraController mainCamera;

		void Start () {
			this.rectTransform = (this.transform as RectTransform);
			blockSize.x = rectTransform.sizeDelta.x / Map.MapManager.Instance.LandX_Num;
			blockSize.y = rectTransform.sizeDelta.y / Map.MapManager.Instance.LandX_Num;
			this.yellowRect.LandSize = Map.MapManager.Instance.GetLandSize();
		}

		void Update () {
			this.yellowRect.SetPos(mainCamera.GetPos());
			if(drag) {
				clickedTime += Time.deltaTime;
			}
		}

		internal void Click() {
			try {
				OnClick();
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		internal void BeginDrag() {
			clickedTime = 0.0f;
			drag = true;
		}

		internal void EndDrag() {
			drag = false;
			Debug.Log(clickedTime);
		}

		public void SetControll(System.Action _click) {
			OnClick = _click;
		}

		public System.Action ClickNoOption() {
			return () => {};
		}

		public System.Action ClickOnCameraOption() {
			return () => {
				Vector2 boardClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
				boardClickPos += this.rectTransform.sizeDelta * 0.5f;
				Vector3 clickLandPos = Map.MapManager.Instance.GetLandPos(
					(int)(boardClickPos.x / this.blockSize.x),
					(int)(boardClickPos.y / this.blockSize.y));
				this.mainCamera.SetPos(new Vector2(clickLandPos.x, clickLandPos.y));
			};
		}

		public System.Action ClickOnMoveOption() {
			return () => {
				Debug.Log("ClickOnMoveOption");
			};
		}

		public System.Action ClickOnAttackOption() {
			return () => {
				Debug.Log("ClickOnAttackOption");
			};
		}
	}
}

