using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.ControllUI.ControllBoard.MiniUnit;

namespace Colonize.Unit.Piece {
	public sealed class PieceController : UnitController<PieceController, PieceStatus, PieceType> {		
		private Piece.PieceManager pieceManager;

		[SerializeField] private PieceStateController stateController;

		private static List<int> pieceNumList = new List<int>();

		public int UnitNum { get { return pieceNumList[(int)this.status.type]; } }
		public PieceStateController StateController { get { return stateController; } }

		void Awake() {
			this.stateController.InitState(this);
			if(!this.photonView.isMine) {
				this.GetComponent<Rigidbody2D>().isKinematic = true;
			}
			if(pieceNumList.Count == 0) {
				for(int i = 0; i < (int)PieceType.End; ++i) {
					pieceNumList.Add(0);
				}
			}
		}
		
		void Start () {

		}

		void Update() {
			
		}

		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, this.status.visualRange);
		}

		internal void SetPieceManager(Piece.PieceManager _pieceManager) {
			pieceManager = _pieceManager;
		}

		internal void SetDead() {
			this.dead = true;
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
			this.ClearObservers();
			if(IsMine()) {
				pieceNumList[(int)this.status.type]--;
			}
		}

		public override int Damaged(int _damage) {
			DamagedOnPhoton(_damage);
			this.photonView.RPC("DamagedOnPhoton", PhotonTargets.Others, _damage);
			return this.status.hp;
		}

		public override void SetData(int _playerId, PieceType _type) {
			SetDataOnPhoton(_playerId, _type);
			this.photonView.RPC("SetDataOnPhoton", PhotonTargets.Others, _playerId, _type);
			pieceNumList[(int)this.status.type]++;
			if(status.type != PieceType.Builder) {
				AddObserver(ControllUI.UnitControll.UnitControllBar.Instance.FindButton(status.type.ToString()));
			}
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
		protected override void SetDataOnPhoton(int _playerId, PieceType _type){
			this.playerId = _playerId;
			if(_playerId != DefaultManager.GameController.Instance.PlayerId) {
				this.pieceManager = DefaultManager.GameController.Instance.GetPlayer(this.playerId).PieceManager;
				this.pieceManager.AddUnit(this);
				this.transform.parent = this.pieceManager.transform;
			}
			this.status = this.pieceManager.UnitInfoDictionary[_type];
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