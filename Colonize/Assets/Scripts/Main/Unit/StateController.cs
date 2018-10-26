using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class StateController<TState, TType, TController, TActionType> : MonoBehaviour
		where TState : UnitAction<TActionType, TController>
		where TType : struct, IComparable, IFormattable, IConvertible
		where TController : MonoBehaviour
		where TActionType : struct, IComparable, IFormattable, IConvertible {
	
		protected List<TState> stateList = new List<TState>();
		protected TState currentState;
		
		protected TController controller;

		internal TController Controller { get { return controller; } }

		internal abstract void InitState(TController _controller);
		internal abstract void ChangeState(TType _type);
	}
}