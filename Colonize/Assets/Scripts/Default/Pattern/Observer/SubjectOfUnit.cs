using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Unit;

namespace Pattern.Observer {
	public abstract class SubjectOfUnit<T> : Photon.MonoBehaviour
		where T : IUnit {
		protected List<IObserverOfUnit<T>> observerList = new List<IObserverOfUnit<T>>();

		protected abstract void Notify();

		public void AddObserver(IObserverOfUnit<T> _observer) {
			observerList.Add(_observer);
		}

		public void RemoveObserver(IObserverOfUnit<T> _observer) {
			observerList.Remove(_observer);
		}

		public void ClearObservers() {
			observerList.Clear();
		}
	}
}