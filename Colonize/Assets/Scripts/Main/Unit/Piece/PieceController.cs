using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.ControllUI.ControllBoard.MiniUnit;

namespace Colonize.Unit.Piece {
	public sealed class PieceController : UnitController<PieceController, PieceStatus, PieceType> {		
		private PieceStateController stateController;

		static private Piece.PieceManager pieceManager;

		void Awake() {
			stateController = new PieceStateController(this);
			stateController.InitState();
			if(!this.photonView.isMine) {
				this.GetComponent<Rigidbody2D>().isKinematic = true;
			}
		}
		
		void Start () {

		}

		void Update() {
			if(this.photonView.isMine) {
				this.stateController.Update();
			}
		}

		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, this.status.visualRange);
		}

		internal static void SetPieceManager(Piece.PieceManager _pieceManager) {
			pieceManager = _pieceManager;
		}

		public void SetMoveState(List<Vector2> _movePosList) {
			this.stateController.SetMovePosList(_movePosList);
			this.stateController.ChangeState(PieceStateType.Move);
		}

		public void SetAttackState(List<Vector2> _movePosList) {
			this.stateController.SetMovePosList(_movePosList);
			this.stateController.ChangeState(PieceStateType.Attack);
		}

		//override
		public override void OnDestroy() {
			
		}

		public override int Damaged(int _damage) {
			DamagedOnPhoton(_damage);
			this.photonView.RPC("DamagedOnPhoton", PhotonTargets.Others, _damage);
			return this.status.hp;
		}

		public override void SetData(int _playerId, PieceType _type) {
			SetDataOnPhoton(_playerId, _type);
			this.photonView.RPC("SetDataOnPhoton", PhotonTargets.Others, _playerId, _type);
			unitNum++;
			this.Notify();
		}

		//Photon
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			
		}

		[PunRPC]
		protected override void SetDataOnPhoton(int _playerId, PieceType _type){
			this.playerId = _playerId;
			this.status = pieceManager.UnitInfoDictionary[_type];
			this.spriteRenderer.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(pieceManager.UnitSpriteNames[this.playerId], this.status.name));
			MiniUnitManager.Instance.CreateMiniUnit(this);
		}

		[PunRPC]
		protected override void DamagedOnPhoton(int _damage) {
			this.status.hp -= _damage;
			if(this.status.hp <= 0) {
				this.dead = true;
				pieceManager.RemoveUnit(this);
			}
		}
	}
}