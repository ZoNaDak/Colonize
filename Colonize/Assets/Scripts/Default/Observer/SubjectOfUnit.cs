using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observer {
	public abstract class SubjectOfUnit<T> : MonoBehaviour 
		where T : SubjectOfUnit<T> {
		protected static List<IObserverOfUnit<T>> observerList = new List<IObserverOfUnit<T>>();

		protected abstract void Notify();

		public void AddObserver(IObserverOfUnit<T> _observer) {
			observerList.Add(_observer);
		}

		public void RemoveObserver(IObserverOfUnit<T> _observer) {
			observerList.Remove(_observer);
		}
	}
}

