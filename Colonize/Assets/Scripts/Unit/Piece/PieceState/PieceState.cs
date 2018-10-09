using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	enum StateType {
		Stand,
		Move,
		Attack,
		End
	}

	public abstract class PieceState {
		private StateType type;

		internal PieceState(StateType _type) {
			this.type = _type;
		}
	}
}

