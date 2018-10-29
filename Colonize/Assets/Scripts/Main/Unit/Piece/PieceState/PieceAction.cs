using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public enum PieceActionType {
		Stand,
		Move,
		Attack,
		Build,
		End
	}

	public abstract class PieceAction : UnitAction<PieceActionType, PieceController> {
		protected static float checkMovePointDist = 10.0f;

		protected readonly PieceStateController stateController;

		internal PieceAction(PieceActionType _type, PieceStateController _stateController)
		 : base(_type) {
			this.stateController = _stateController;
		}

		internal override abstract void StartState();
		internal override abstract void StopState();
	}
}