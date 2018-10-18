using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public sealed class PieceController : UnitController<PieceController, PieceStatus> {		
		private PieceStateController stateController;

		void Awake() {
			stateController = new PieceStateController(this);
			stateController.InitState();
		}
		
		void Start () {

		}

		void Update () {
			
		}

		[PunRPC]
		protected override void SetDataOnPhoton(int _playerId, PieceStatus _status, string _spriteName){
			this.playerId = _playerId;
			this.status = _status;
			this.spriteRenderer.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(_spriteName, _status.name));;
		}

		public void SetMoveState(List<Vector2> _movePosList) {
			this.stateController.SetMovePosList(_movePosList);
			this.stateController.ChangeState(PieceStateType.Move);
		}

		//Photon
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		}

		public override void SetData(int _playerId, PieceStatus _status, string _spriteName) {
			this.photonView.RPC("SetDataOnPhoton", PhotonTargets.AllBuffered, _playerId, _status, _spriteName);
			unitNum++;
			this.Notify();
		}
	}
}