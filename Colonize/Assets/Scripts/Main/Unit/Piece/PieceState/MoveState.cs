using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
    public class MoveState : PieceState {
        private static float CheckDist = 10.0f;

        internal MoveState(PieceStateController _stateController)
         : base(PieceStateType.Stand, _stateController){

        }

        internal IEnumerator MoveCoroutine() {
            while (true) {
                Vector2 moveDir = this.stateController.CurrentMovePos - (Vector2)this.stateController.Controller.transform.position;
                if(moveDir.magnitude <= CheckDist) {
                    this.stateController.SetCurrentMovePosToNext();
                    if(this.stateController.MovePosList == null) {
                        this.stateController.ChangeState(PieceStateType.Stand);
                        break;
                    } else {
                        moveDir = this.stateController.CurrentMovePos - (Vector2)this.stateController.Controller.transform.position;
                    }
                }
                this.stateController.Controller.transform.Translate(moveDir.normalized * this.stateController.Controller.Status.speed);
                yield return new WaitForSecondsRealtime(0.05f);
            }
        }

        internal override void StartState() {
            if(!this.stateController.Controller.photonView.isMine) {
                return;
            }
            this.coroutine = this.stateController.Controller.StartCoroutine(MoveCoroutine());
        }

        internal override void StopState() {
            this.stateController.Controller.StopCoroutine(this.coroutine);
        }
    }
}