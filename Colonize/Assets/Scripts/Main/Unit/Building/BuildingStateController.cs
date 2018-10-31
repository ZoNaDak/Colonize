using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Colonize.Unit.Building {
    public enum BuildingStateType {
        Stop,
        Produce,
        End
    }
	public abstract class BuildingStateController : StateController<BuildingAction, BuildingController, BuildingActionType> {
		protected BuildingStateType stateType;

		public BuildingStateType StateType { get { return stateType; } }

		void Awake() {
			
		}

		internal void StartAllActions() {
            for(int i = 0; i < (int)BuildingActionType.End; ++i) {
                if(this.actionList[i] != null) {
                    this.actionList[i].StartAction();
                }
			}
        }

        internal void StopAllActions() {
            for(int i = 0; i < (int)BuildingActionType.End; ++i) {
                if(this.actionList[i] != null) {
                    this.actionList[i].StopAction();
                }
			}
        }

		//abstract override
		internal abstract override void InitState(BuildingController _controller);
	}
}