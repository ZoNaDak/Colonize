using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class StateController<TAction, TController, TActionType> : MonoBehaviour
		where TAction : UnitAction<TActionType, TController>
		where TController : MonoBehaviour
		where TActionType : struct, IComparable, IFormattable, IConvertible {
	
		protected List<TAction> actionList = new List<TAction>();
		protected TAction currentAction;
		
		protected TController controller;

		internal TController Controller { get { return controller; } }

		internal abstract void InitState(TController _controller);
	}
}