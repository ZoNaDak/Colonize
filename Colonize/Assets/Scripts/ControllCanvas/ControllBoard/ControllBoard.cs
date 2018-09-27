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

		private ClickDelegate OnClick;

		[SerializeField] private YellowRect yellowRect;
		[SerializeField] private Camera mainCamera;

		void Start () {
			this.rectTransform = (this.transform as RectTransform);
			blockSize.x = rectTransform.sizeDelta.x / Map.MapManager.Instance.LandX_Num;
			blockSize.y = rectTransform.sizeDelta.y / Map.MapManager.Instance.LandX_Num;
			this.yellowRect.LandSize = Map.MapManager.Instance.GetLandSize();
		}

		void Update () {
			this.yellowRect.SetPos(mainCamera.transform.position);
			if(drag) {
				clickedTime += Time.deltaTime;
			}
		}

		public void Click() {
			try {
				OnClick();
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		public void BeginDrag() {
			clickedTime = 0.0f;
			drag = true;
		}

		public void EndDrag() {
			drag = false;
			Debug.Log(clickedTime);
		}

		public void SetControll(ClickDelegate _click) {
			OnClick = _click;
		}

		public ClickDelegate ClickOnCameraOption() {
			return () => {
				Vector2 boardClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
				boardClickPos += this.rectTransform.sizeDelta * 0.5f;
				Vector3 clickLandPos = Map.MapManager.Instance.GetLandPos(
					(int)(boardClickPos.x / this.blockSize.x),
					(int)(boardClickPos.y / this.blockSize.y));
				this.mainCamera.transform.position = new Vector3(
					clickLandPos.x,
					clickLandPos.y,
					this.mainCamera.transform.position.z
				);
			};
		}

		public ClickDelegate ClickOnMoveOption() {
			return () => {
				Debug.Log("ClickOnMoveOption");
			};
		}

		public ClickDelegate ClickOnAttackOption() {
			return () => {
				Debug.Log("ClickOnAttackOption");
			};
		}
	}
}

