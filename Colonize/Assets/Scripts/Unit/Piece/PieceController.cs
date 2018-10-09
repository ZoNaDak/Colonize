using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public sealed class PieceController : UnitController<PieceController, PieceStatus> {
		//private List<Vector2> movePosList = new List<Vector2>();
		private List<PieceState> stateList = new List<PieceState>((int)StateType.End - 1);

		void Awkae() {
			InitStateList();
		}
		
		void Start () {

		}

		void Update () {
			
		}

		private void InitStateList() {
			for(int i = 0; i < stateList.Capacity; ++i) {
				switch((StateType)i) {
					case StateType.Stand:
					break;
					case StateType.Move:
					break;
					case StateType.Attack:
					break;
					default:
						throw new System.NotImplementedException("Not Impelement InitStateType In Switch Sentence");
				}
			}
		}

		private void Move() {

		}

		public override void SetData(int _playerId, PieceStatus _status, Sprite _sprite){
			this.playerId = _playerId;
			this.status = _status;
			this.spriteRenderer.sprite = _sprite;
			unitNum++;
			this.Notify();
		}

		public void SetMovePosList(List<Vector2> _movePosList) {
			try {
				if(_movePosList == null) {
					throw new System.ArgumentNullException("MovePosList is Null");
				}
				
			} catch (System.ArgumentNullException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
			
		}
	}
}