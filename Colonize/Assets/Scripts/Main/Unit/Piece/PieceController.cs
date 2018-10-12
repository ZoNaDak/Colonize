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

		public override void SetData(int _playerId, PieceStatus _status, Sprite _sprite){
			this.playerId = _playerId;
			this.status = _status;
			this.spriteRenderer.sprite = _sprite;
			unitNum++;
			this.Notify();
		}

		public void SetMoveState(List<Vector2> _movePosList) {
			this.stateController.SetMovePosList(_movePosList);
			this.stateController.ChangeState(PieceStateType.Move);
		}
	}
}