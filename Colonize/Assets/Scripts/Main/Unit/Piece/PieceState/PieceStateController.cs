using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public enum PieceStateType {
		Stand,
		Move,
		Attack,
		End
	}

	public abstract class PieceStateController : StateController<PieceAction, PieceStateType, PieceController, PieceActionType> {
		protected List<Vector2> movePosList;
		protected int currentMovePosIdx;
		protected bool isAttackalbe;
		protected PieceStateType stateType;
		protected IUnit targetUnit;

		protected static int checkableLayerMask;

		internal IUnit TargetUnit { get { return targetUnit; } }

		public List<Vector2> MovePosList { get { return movePosList; } }
		public PieceStateType StateType { get { return stateType; } }

		public Vector2 CurrentMovePos { 
			get {
				if(currentMovePosIdx >= movePosList.Count) {
					return movePosList.Last();
				} 
				return movePosList[currentMovePosIdx];
			} 
		}

		void Awake() {
			checkableLayerMask = ~((1 << LayerMask.NameToLayer("Building")) | (1 << LayerMask.NameToLayer("Piece")));
		}

		protected void ChangeAction(PieceActionType _type) {
			if(this.currentState != null) {
				this.currentState.StopState();
			}
			this.currentState = this.stateList[(int)_type];
			this.currentState.StartState();
		}

		internal void SetCurrentMovePosToNext() {
			this.currentMovePosIdx++;
			if(this.currentMovePosIdx >= this.movePosList.Count) {
				this.movePosList = null;
				this.currentMovePosIdx = -1;
			}
		}

		internal bool CheckIsEnemyAroundHere() {
			Collider2D[] unitInRange = Physics2D.OverlapCircleAll(this.controller.transform.position, this.controller.Status.visualRange, checkableLayerMask);
			var enemiesInRange = from enemy in unitInRange
				where enemy.gameObject != this.controller.gameObject && !enemy.GetComponentInChildren<IUnit>().IsMine() && !enemy.GetComponentInChildren<IUnit>().GetDead()
				select enemy.GetComponentInChildren<IUnit>();
			float minDist = Mathf.Infinity;
			bool check = false;
			foreach(var enemy in enemiesInRange) {
				float dist = Vector2.Distance(this.controller.transform.position, enemy.GetPos());
				if(dist < minDist) {
					minDist = dist;
					targetUnit = enemy;
					check = true;
				}
			}
			return check;
		}

		public void SetMovePosList(List<Vector2> _movePosList) {
			try {
				if(_movePosList == null) {
					throw new System.ArgumentNullException("MovePosList is Null");
				}
				this.movePosList = _movePosList;
				this.currentMovePosIdx = 0;
			} catch (System.ArgumentNullException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
		}

		//abstract override
		internal abstract override void InitState(PieceController _controller);
		internal abstract override void ChangeState(PieceStateType _type);
	}
}