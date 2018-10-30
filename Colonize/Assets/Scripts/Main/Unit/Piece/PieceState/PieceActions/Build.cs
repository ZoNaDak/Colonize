using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.DefaultManager;

namespace Colonize.Unit.Piece {
    public class Build : PieceAction {
        internal Build(PieceStateController _stateController)
         : base(PieceActionType.Build, _stateController){

        }

        internal IEnumerator BuildCoroutine() {
            yield return new WaitForSecondsRealtime(0.01f);
            Vector2Int landIdx = Map.MapManager.Instance.GetLandIdx(this.stateController.Controller.transform.position);
            GameController.Instance.MyPlayer.BuildingManager.CreateUnit((this.stateController as BuilderStateController).BuildingType, landIdx);
            this.stateController.Controller.SetDead();
            GameController.Instance.MyPlayer.PieceManager.RemoveUnit(this.stateController.Controller);
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