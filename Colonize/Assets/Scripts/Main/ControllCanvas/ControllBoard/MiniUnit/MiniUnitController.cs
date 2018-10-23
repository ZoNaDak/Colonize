using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Colonize.Unit;

namespace Colonize.ControllUI.ControllBoard.MiniUnit {
	public abstract class MiniUnitController<TController, TStatus, TType> : MonoBehaviour 
		where TController : class
		where TStatus : struct
		where TType : struct, IComparable, IFormattable, IConvertible {
		
		protected UnitController<TController, TStatus, TType> unitController;

		[SerializeField] protected Image unitImage;

		public abstract void CheckDead();

		public abstract void Initialize(int _playerId, TController _controller);
		public abstract void RemoveSelf();
	}
}