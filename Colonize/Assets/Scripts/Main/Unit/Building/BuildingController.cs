using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.ControllUI.ControllBoard.MiniUnit;

namespace Colonize.Unit.Building {
	public sealed class BuildingController : UnitController<BuildingController, BuildingStatus, BuildingType> {
		private static List<int> buildingNumList = new List<int>();

		private Building.BuildingManager buildingManager;
		private Piece.PieceManager pieceManager;

		[SerializeField] private BuildingStateController stateController;

		internal Piece.PieceManager PieceManager { get { return pieceManager; } }
		public int UnitNum { get { return buildingNumList[(int)this.status.type]; } }

		void Awake() {
			if(buildingNumList.Count == 0) {
				for(int i = 0; i < (int)BuildingType.End; ++i) {
					buildingNumList.Add(0);
				}
			}
		}

		void Start () {
			this.stateController.InitState(this);
		}

		void Update () {
			
		}

		internal void SetBuildingManager(BuildingManager _buildingManager) {
			this.buildingManager = _buildingManager;
		}

		internal void SetPieceManager(Piece.PieceManager _pieceManager) {
			this.pieceManager = _pieceManager;
		}

		//override
		public override void OnDestroy() {
			this.ClearObservers();
			if(IsMine()) {
				buildingNumList[(int)this.status.type]--;
			}
		}

		public override int Damaged(int _damage) {
			DamagedOnPhoton(_damage);
			this.photonView.RPC("DamagedOnPhoton", PhotonTargets.Others, _damage);
			return this.status.hp;
		}

		public override void SetData(int _playerId, BuildingType _type) {
			SetDataOnPhoton(_playerId, _type);
			this.photonView.RPC("SetDataOnPhoton", PhotonTargets.Others, _playerId, _type);
			buildingNumList[(int)this.status.type]++;
			AddObserver(ControllUI.UnitControll.UnitControllBar.Instance.FindButton(string.Format("Build{0}", status.type.ToString())));
			this.Notify();
		}

		//interface override
		public override int GetNum() {
			return this.UnitNum;
		}

		//Photon
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		}

		[PunRPC]
		protected override void SetDataOnPhoton(int _playerId, BuildingType _type) {
			this.playerId = _playerId;
			if(_playerId != DefaultManager.GameController.Instance.PlayerId) {
				this.buildingManager = DefaultManager.GameController.Instance.GetPlayer(this.playerId).BuildingManager;
				this.pieceManager = DefaultManager.GameController.Instance.GetPlayer(this.playerId).PieceManager;
				this.buildingManager.AddUnit(this);
				this.transform.parent = this.buildingManager.transform;
			}
			this.status = this.buildingManager.UnitInfoDictionary[_type];
			this.spriteRenderer.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(buildingManager.UnitSpriteNames[this.playerId], this.status.name));
			MiniUnitManager.Instance.CreateMiniUnit(this);
		}

		[PunRPC]
		protected override void DamagedOnPhoton(int _damage) {
			this.status.hp -= _damage;
			if(this.status.hp <= 0) {
				this.dead = true;
				buildingManager.RemoveUnit(this);
			}
		}
	}
}

