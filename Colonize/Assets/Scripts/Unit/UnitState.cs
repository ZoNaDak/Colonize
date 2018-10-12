using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class UnitState<TType, TController> 
		where TType : struct, IComparable, IFormattable, IConvertible {
		public readonly TType type;

		protected Coroutine coroutine;

		internal UnitState(TType _type) {
			this.type = _type;
		}

		internal abstract void StartState();
		internal abstract void StopState();
	}
}

