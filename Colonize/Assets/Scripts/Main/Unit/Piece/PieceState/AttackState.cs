using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
    public class Attack : PieceAction {
        internal Attack(PieceStateController _stateController)
         : base(PieceActionType.Attack, _stateController){

        }

        internal IEnumerator AttackCoroutine() {
            while (true) {
                if(this.stateController.TargetUnit.GetDead()) {
                    if(this.stateController.MovePosList == null) {
                        this.stateController.ChangeState(PieceStateType.Stand);
                    } else {
                        this.stateController.ChangeState(PieceStateType.Move);
                    }
                    break;
                }
                Vector2 moveDir = this.stateController.TargetUnit.GetPos() - (Vector2)this.stateController.Controller.transform.position;
                if(moveDir.magnitude <= this.stateController.Controller.Status.attackRange) {
                    this.stateController.TargetUnit.Damaged(this.stateController.Controller.Status.attack);
                    yield return new WaitForSecondsRealtime(this.stateController.Controller.Status.attackCooltime);
                } else {
                    this.stateController.Controller.transform.Translate(moveDir.normalized * this.stateController.Controller.Status.speed);
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
        }  

        internal override void StartState() {
            if(!this.stateController.Controller.photonView.isMine) {
                return;
            }
            this.coroutine = this.stateController.Controller.StartCoroutine(AttackCoroutine());
        }

        internal override void StopState() {
            if(this.coroutine != null) {
                this.stateController.Controller.StopCoroutine(this.coroutine);
            }
        }
    }
}