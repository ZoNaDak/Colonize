using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public enum PieceActionType {
		Stand,
		Move,
		Attack,
		Shoot,
		Build,
		End
	}

	public abstract class PieceAction : UnitAction<PieceActionType, PieceController> {
		protected static float checkMovePointDist = 20.0f;

		protected readonly PieceStateController stateController;

		internal PieceAction(PieceActionType _type, PieceStateController _stateController)
		 : base(_type) {
			this.stateController = _stateController;
		}

		internal override abstract void StartAction();
		internal override abstract void StopAction();
	}
}