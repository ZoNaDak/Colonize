using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observer {
	public interface IObserverOfUnit<TSubject> {
        void OnNotify(TSubject _subject);
    }
}