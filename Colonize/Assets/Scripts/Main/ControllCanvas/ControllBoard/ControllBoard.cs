using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.ControllUI.ControllBoard {
	public class ControllBoard : Pattern.Singleton.MonoSingleton<ControllBoard> {
		public delegate void ClickDelegate();

		private RectTransform rectTransform;
		private Vector2 blockSize = new Vector2();
		private float clickedTime;
		private bool drag = false;
		private Unit.Piece.PieceType selectedPieceType;

		private System.Action OnClick;

		[SerializeField] private YellowRect yellowRect;
		[SerializeField] private MyCamera.MainCameraController mainCamera;
		[SerializeField] private Unit.Piece.PieceManager pieceManager;

		void Start () {
			this.rectTransform = (this.transform as RectTransform);
			blockSize.x = rectTransform.sizeDelta.x / Map.MapManager.Instance.LandNumX;
			blockSize.y = rectTransform.sizeDelta.y / Map.MapManager.Instance.LandNumY;
			this.yellowRect.LandSize = Map.MapManager.Instance.GetLandSize();
		}

		void Update () {
			this.yellowRect.SetPos(mainCamera.GetPos());
			if(drag) {
				clickedTime += Time.deltaTime;
			}
		}

		private Vector2 GetLandPosForClickBoard() {
			Vector2 boardClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
			boardClickPos += this.rectTransform.sizeDelta * 0.5f;
			Vector2 clickLandPos = Map.MapManager.Instance.GetLandPos(
				(int)(boardClickPos.x / this.blockSize.x),
				(int)(boardClickPos.y / this.blockSize.y));

			return clickLandPos;
		}

		private Vector2Int GetLandIdxForClickBoard() {
			Vector2 boardClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
			boardClickPos += this.rectTransform.sizeDelta * 0.5f;
			Vector2Int clickLandIdx = new Vector2Int(
				(int)(boardClickPos.x / this.blockSize.x),
				(int)(boardClickPos.y / this.blockSize.y));

			return clickLandIdx;
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

		public void SetControll(System.Action _click, Unit.Piece.PieceType _pieceType) {
			this.OnClick = _click;
			this.selectedPieceType = _pieceType;
		}

		public System.Action ClickNoOption() {
			return () => {};
		}

		public System.Action ClickOnCameraOption() {
			return () => {
				this.mainCamera.SetPos(GetLandPosForClickBoard());
			};
		}

		public System.Action ClickOnMoveOption() {
			return () => {
				this.pieceManager.MovePieces(selectedPieceType, GetLandIdxForClickBoard());
			};
		}

		public System.Action ClickOnAttackOption() {
			return () => {
				Debug.Log("ClickOnAttackOption");
			};
		}
	}
}

