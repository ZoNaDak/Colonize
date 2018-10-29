using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class UnitController<T, TStatus, TType> : Pattern.Observer.SubjectOfUnit<IUnit>, IUnit
		where T : class
		where TStatus : struct
		where TType : struct, IComparable, IFormattable, IConvertible{

		protected TStatus status;
		protected int playerId;
		protected bool dead;

		[SerializeField] protected SpriteRenderer spriteRenderer;

		public int PlayerId { get { return playerId; } }
		public TStatus Status { get { return status; } }
		public bool Dead { get { return dead; } }

		public abstract void SetData(int _playerId, TType _type);

		//Interface
		public abstract void OnDestroy();
		public abstract int Damaged(int _damage);
		
		public bool IsMine() {
			return this.photonView.isMine; 
		}

		public bool GetDead() {
			return this.dead;
		}

		public int GetPlayerId() {
			return this.playerId;
		}

		public Vector2 GetPos() {
			return this.transform.position;
		}

		public GameObject GetGameObject() {
			return this.gameObject;
		}

		public abstract int GetNum();

		//Observer
		protected override void Notify() {
			for(int i = 0; i < observerList.Count; ++i) {
				observerList[i].OnNotify(this);
			}
		}

		//Photon
		[PunRPC] protected abstract void SetDataOnPhoton(int _playerId, TType _type);
		[PunRPC] protected abstract void DamagedOnPhoton(int _damage);
	}
}

