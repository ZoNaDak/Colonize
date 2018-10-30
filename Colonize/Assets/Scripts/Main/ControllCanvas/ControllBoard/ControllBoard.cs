using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.DefaultManager;
using Colonize.Unit.Building;
using Colonize.Unit.Piece;

namespace Colonize.ControllUI.ControllBoard {
	public class ControllBoard : Pattern.Singleton.MonoSingleton<ControllBoard> {
		public delegate void ClickDelegate();

		private RectTransform rectTransform;
		private Vector2 blockSize = new Vector2();
		private float clickedTime;
		private bool drag = false;
		private BuildingManager buildingManager;
		private PieceManager pieceManager;
		private PieceType selectedPieceType;
		private BuildingType selectedBuildingType;
		private int selectedBuidlingCost;
		
		private System.Action OnClick;

		[SerializeField] private YellowRect yellowRect;
		[SerializeField] private MyCamera.MainCameraController mainCamera;

		void Start () {
			StartCoroutine(Initialize());
		}

		void Update () {
			this.yellowRect.SetPos(mainCamera.GetPos());
			if(drag) {
				clickedTime += Time.deltaTime;
			}
		}

		private IEnumerator Initialize() {
			yield return new WaitUntil(() => GameController.Instance.Ready);
			this.buildingManager = GameController.Instance.MyPlayer.BuildingManager;
			this.pieceManager = GameController.Instance.MyPlayer.PieceManager;
			this.rectTransform = (this.transform as RectTransform);
			this.blockSize = new Vector2(
				rectTransform.sizeDelta.x / Map.MapManager.Instance.LandNumX,
				blockSize.y = rectTransform.sizeDelta.y / Map.MapManager.Instance.LandNumY);
			this.yellowRect.LandSize = Map.MapManager.Instance.GetLandSize();
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

		public void SetControll(System.Action _click, PieceType _pieceType) {
			this.OnClick = _click;
			this.selectedPieceType = _pieceType;
		}

		public void SetControll(System.Action _click, BuildingType _buildingType, int _buildCost) {
			this.OnClick = _click;
			this.selectedBuildingType = _buildingType;
			this.selectedBuidlingCost = _buildCost;
		}

		public Vector2 GetBoardPosFromWorldPos(Vector2 _worldPosition) {
			Vector2 returnPos = _worldPosition + Map.MapManager.Instance.GetLandSize() * 0.5f;
			returnPos /= Map.MapManager.Instance.GetLandSize();
			returnPos *= (this.rectTransform.sizeDelta / new Vector2(Map.MapManager.Instance.LandNumX, Map.MapManager.Instance.LandNumY));
			returnPos -= this.rectTransform.sizeDelta * 0.5f;
			return returnPos;
		}

		//Delegate
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
				this.pieceManager.AttackPieces(selectedPieceType, GetLandIdxForClickBoard());
			};
		}

		public System.Action ClickOnBuildOption() {
			return () => {
				if(GameController.Instance.MyPlayer.Gold < this.selectedBuidlingCost) {
					GameController.Instance.SetNotifyText("Warning : Not enough Gold");
					return;
				}
				if(this.buildingManager.CheckIsBuildingInLand(GetLandIdxForClickBoard())) {
					GameController.Instance.SetNotifyText("Warning : Already building is click land");
					return;
				}
				
				GameController.Instance.MyPlayer.Gold -= this.selectedBuidlingCost;
				Vector2 clickLandPos = GetLandPosForClickBoard();
				Vector2 createPos = this.buildingManager.NearestBuilding(clickLandPos).transform.position;
				createPos += (clickLandPos - createPos).normalized * 80.0f;
				PieceController piece = this.pieceManager.CreateUnit(Unit.Piece.PieceType.Builder, createPos);
				(piece.StateController as BuilderStateController).SetBuildingType(this.selectedBuildingType);
				this.pieceManager.MovePiece(piece, GetLandIdxForClickBoard());
			};
		}
	}
}

