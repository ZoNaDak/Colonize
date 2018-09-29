using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building {
	public enum BuildingType {
		Commander,
		End
	}

	public struct BuildingStatus {
		public readonly BuildingType type;
		public readonly string name;
		public readonly int hp;

		public BuildingStatus(BuildingType _type, string _name, int _hp) {
			this.type = _type;
			this.name = _name;
			this.hp = _hp;
		}
	}
}