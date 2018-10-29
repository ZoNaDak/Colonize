using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public class BuilderStateController : PieceStateController {

		void Update () {
			if(IsArrived()) {
				ChangeAction(PieceActionType.Build);
			}
		}

		public bool IsArrived() {
			return false;
		}

		//override
		internal override void InitState(PieceController _controller) {
			this.controller = _controller;

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
					case PieceActionType.Build:
						this.stateList[i] = new Build(this);
					break;
					default:
					continue;
				}
			}
			ChangeState(PieceStateType.Stand);
		}

		internal override void ChangeState(PieceStateType _type) {
			switch(_type) {
				case PieceStateType.Stand:
					this.isAttackalbe = false;
					ChangeAction(PieceActionType.Stand);
				break;
				case PieceStateType.Move:
					this.isAttackalbe = false;
					ChangeAction(PieceActionType.Move);
				break;
				default:
					throw new System.ArgumentException("PieceStateType is Not Correctable! : " + _type);
			}
			stateType = _type;
		}
	}
}