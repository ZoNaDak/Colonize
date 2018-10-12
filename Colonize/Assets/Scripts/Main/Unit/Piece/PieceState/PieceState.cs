using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public enum PieceStateType {
		Stand,
		Move,
		Attack,
		End
	}

	public abstract class PieceState : UnitState<PieceStateType, PieceController> {

		protected readonly PieceStateController stateController;

		internal PieceState(PieceStateType _type, PieceStateController _stateController)
		 : base(_type) {
			this.stateController = _stateController;
		}

		internal override abstract void StartState();
		internal override abstract void StopState();
	}
}

