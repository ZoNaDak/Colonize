using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit {
	public abstract class UnitController<T, TStatus> : Observer.SubjectOfUnit<UnitController<T, TStatus>>
		where T : class where TStatus : struct {
		
		protected static int unitNum;

		protected TStatus status;
		protected int playerId;
		
		protected bool dead;
		protected int unitHp;

		[SerializeField] protected SpriteRenderer spriteRenderer;

		public int UnitNum { get { return unitNum; } }
		public int PlayerId { get { return playerId; } }
		public TStatus Status { get { return status; } }
		public bool Dead { get { return dead; } }

		public abstract void SetData(int _playerId, TStatus _status, Sprite _sprte);

		protected override void Notify() {
			for(int i = 0; i < observerList.Count; ++i) {
				observerList[i].OnNotify(this);
			}
		}
	}
}

