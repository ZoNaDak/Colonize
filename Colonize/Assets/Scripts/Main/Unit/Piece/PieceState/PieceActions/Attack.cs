using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
    public class Attack : PieceAction {
        private PieceController controller;

        private static int checkableLayerMask = ~((1 << LayerMask.NameToLayer("Building")) | (1 << LayerMask.NameToLayer("Piece")));

        internal Attack(PieceStateController _stateController)
         : base(PieceActionType.Attack, _stateController){
            this.controller = _stateController.Controller;
        }

        private bool Attackable() {
            Collider2D[] inAttackRange = Physics2D.OverlapCircleAll(this.controller.transform.position, this.controller.Status.attackRange, checkableLayerMask);
            for(int i = 0; i < inAttackRange.Length; ++i) {
                if(inAttackRange[i].gameObject == this.stateController.TargetUnit.GetGameObject()) {
                    return true;
                }
            }

            return false;
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
                
                if(this.Attackable()) {
                    this.stateController.TargetUnit.Damaged(this.controller.Status.attack);
                    yield return new WaitForSecondsRealtime(this.controller.Status.attackCooltime);
                } else {
                    Vector2 moveDir = this.stateController.TargetUnit.GetPos() - (Vector2)this.controller.transform.position;
                    this.controller.transform.Translate(moveDir.normalized * this.controller.Status.speed);
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
        }  

        internal override void StartState() {
            if(!this.controller.photonView.isMine) {
                return;
            }
            this.coroutine = this.controller.StartCoroutine(AttackCoroutine());
        }

        internal override void StopState() {
            if(this.coroutine != null) {
                this.controller.StopCoroutine(this.coroutine);
            }
        }
    }
}