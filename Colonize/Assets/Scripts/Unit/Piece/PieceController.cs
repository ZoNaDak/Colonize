using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;

namespace Piece {
	public sealed class PieceController : UnitController<PieceController, PieceStatus> {
		
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
	}
}