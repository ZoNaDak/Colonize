using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
    public class Build : PieceAction {
        internal Build(PieceStateController _stateController)
         : base(PieceActionType.Move, _stateController){

        }

        internal IEnumerator BuildCoroutine() {
            while (true) {
                
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }

        internal override void StartState() {
            if(!this.stateController.Controller.photonView.isMine) {
                return;
            }
            this.coroutine = this.stateController.Controller.StartCoroutine(BuildCoroutine());
        }

        internal override void StopState() {
            if(this.coroutine != null) {
                this.stateController.Controller.StopCoroutine(this.coroutine);
            }
        }
    }
}