using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public enum PieceType {
		SwordMan,
		End
	}

	public struct PieceStatus {
		public readonly PieceType type;
		public readonly string name;
		public readonly int hp;
		public readonly int attack;
		public readonly int speed;

		public PieceStatus(PieceType _type, string _name, int _hp, int _attack, int _speed) {
			this.type = _type;
			this.name = _name;
			this.hp = _hp;
			this.attack = _attack;
			this.speed = _speed;
		}
	}
}

