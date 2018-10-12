using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class StateController<TState, TType, TController> 
		where TState : UnitState<TType, TController>
		where TType : struct, IComparable, IFormattable, IConvertible
		where TController : MonoBehaviour {
	
		protected List<TState> stateList = new List<TState>();
		protected TState currentState;
		
		protected readonly TController controller;

		internal TController Controller { get { return controller; } }

		public StateController(TController _controller) {
			this.controller = _controller;
		}

		internal abstract void InitState();
		internal abstract void ChangeState(TType _type);
	}
}