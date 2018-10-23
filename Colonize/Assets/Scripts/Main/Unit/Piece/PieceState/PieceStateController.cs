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

	public class PieceStateController : StateController<PieceAction, PieceStateType, PieceController, PieceActionType> {
		private List<Vector2> movePosList;
		private int currentMovePosIdx;
		private bool isAttackalbe;
		private PieceStateType stateType;
		private IUnit targetUnit;

		private static int checkableLayerMask = ~((1 << LayerMask.NameToLayer("Building")) | (1 << LayerMask.NameToLayer("Piece")));

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

		public PieceStateController(PieceController _controller)
		 : base(_controller) {

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

		//override
		internal override void InitState() {
			for(int i = 0; i < (int)PieceActionType.End; ++i) {
				this.stateList.Add(null);
			}

			for(int i = 0; i < (int)PieceActionType.End; ++i) {
				switch((PieceActionType)i) {
					case PieceActionType.Stand:
						this.stateList[i] = new Stand(this);
					break;
					case PieceActionType.Move:
						this.stateList[i] = new Move(this);
					break;
					case PieceActionType.Attack:
						this.stateList[i] = new Attack(this);
					break;
					default:
						throw new System.NotImplementedException("Not Impelement InitStateType In Switch Sentence");
				}
			}
			this.currentState = this.stateList[(int)PieceStateType.Stand];
		}

		internal override void ChangeState(PieceStateType _type) {
			switch(_type) {
				case PieceStateType.Stand:
					this.currentState.StopState();
					this.isAttackalbe = true;
					this.currentState = this.stateList[(int)PieceActionType.Stand];
					this.currentState.StartState();
				break;
				case PieceStateType.Move:
					this.currentState.StopState();
					this.isAttackalbe = false;
					this.currentState = this.stateList[(int)PieceActionType.Move];
					this.currentState.StartState();
				break;
				case PieceStateType.Attack:
					this.currentState.StopState();
					this.isAttackalbe = true;
					this.currentState = this.stateList[(int)PieceActionType.Move];
					this.currentState.StartState();
				break;
				default:
					throw new System.ArgumentException("PieceStateType is Not Correctable! : " + _type);
			}
			stateType = _type;
		}

		internal override void Update() {
			if(this.isAttackalbe) {
				if(this.currentState.Type != PieceActionType.Attack && CheckIsEnemyAroundHere()){
					this.currentState.StopState();
					this.currentState = this.stateList[(int)PieceActionType.Attack];
					this.currentState.StartState();
				}
			}
		}
	}
}