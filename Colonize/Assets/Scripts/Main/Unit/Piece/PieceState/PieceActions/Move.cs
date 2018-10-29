using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
    public class Move : PieceAction {
        internal Move(PieceStateController _stateController)
         : base(PieceActionType.Move, _stateController){

        }

        internal IEnumerator MoveCoroutine() {
            while (true) {
                if(this.stateController.MovePosList == null || this.stateController.MovePosList.Count == 0) {
                    this.stateController.ChangeState(PieceStateType.Stand);
                    break;
                }
                Vector2 moveDir = this.stateController.CurrentMovePos - (Vector2)this.stateController.Controller.transform.position;
                this.stateController.Controller.transform.Translate(moveDir.normalized * this.stateController.Controller.Status.speed);
                Vector2 distance = this.stateController.CurrentMovePos - (Vector2)this.stateController.Controller.transform.position;
                if(distance.magnitude <= checkMovePointDist) {
                    this.stateController.SetCurrentMovePosToNext();
                }
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }

        //override
        internal override void StartState() {
            if(!this.stateController.Controller.photonView.isMine) {
                return;
            }
            this.coroutine = this.stateController.Controller.StartCoroutine(MoveCoroutine());
        }

        internal override void StopState() {
            if(this.coroutine != null) {
                this.stateController.Controller.StopCoroutine(this.coroutine);
            }
        }
    }
}