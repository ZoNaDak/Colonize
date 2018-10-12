using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattern.Observer {
	public interface IObserverOfUnit<TSubject> {
        void OnNotify(TSubject _subject);
    }
}