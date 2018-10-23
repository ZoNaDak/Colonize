using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class UnitAction<TActionType, TController>
		where TActionType : struct, IComparable, IFormattable, IConvertible {

		protected TActionType type;
		protected Coroutine coroutine;

		public TActionType Type { get { return type; } }

		internal UnitAction(TActionType _type) {
			this.type = _type;
		}

		internal abstract void StartState();
		internal abstract void StopState();
	}
}

