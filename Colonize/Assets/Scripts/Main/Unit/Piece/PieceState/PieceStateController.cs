using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public class PieceStateController : StateController<PieceState, PieceStateType, PieceController> {

		private List<Vector2> movePosList;
		private int currentMovePosIdx;

		public List<Vector2> MovePosList { get { return movePosList; } }
		public Vector2 CurrentMovePos { get { return movePosList[currentMovePosIdx]; } }

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

		internal override void InitState() {
			for(int i = 0; i < (int)PieceStateType.End; ++i) {
				this.stateList.Add(null);
			}
			for(int i = 0; i < (int)PieceStateType.End; ++i) {
				switch((PieceStateType)i) {
					case PieceStateType.Stand:
						this.stateList[i] = new StandState(this);
					break;
					case PieceStateType.Move:
						this.stateList[i] = new MoveState(this);
					break;
					case PieceStateType.Attack:
						this.stateList[i] = new AttackState(this);
					break;
					default:
						throw new System.NotImplementedException("Not Impelement InitStateType In Switch Sentence");
				}
			}
			this.currentState = this.stateList[(int)PieceStateType.Stand];
		}

		internal override void ChangeState(PieceStateType _type) {
			this.currentState.StopState();
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
	}
}