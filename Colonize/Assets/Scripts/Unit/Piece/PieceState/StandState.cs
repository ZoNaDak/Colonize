using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
    public class StandState : PieceState {
        internal StandState(PieceStateController _stateController)
         : base(PieceStateType.Stand, _stateController){

        }

        internal override void StartState() {

        }

        internal override void StopState() {

        }
    }
}