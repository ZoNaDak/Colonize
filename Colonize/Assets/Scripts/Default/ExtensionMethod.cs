using System;
using UnityEngine;

namespace ExtensionMethod {
	static class ExtensionMethodOfVector {
		public static bool CheckOutRange(this Vector2 _vector, Vector2 _range) {
			return _vector.x < 0.0f || _vector.x > _range.x ||
					_vector.y < 0.0f || _vector.y > _range.y; 
		}
	}
}

