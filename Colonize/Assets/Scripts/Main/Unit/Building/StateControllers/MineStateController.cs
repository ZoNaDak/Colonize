using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Colonize.Unit.Building {
	public class MineStateController : BuildingStateController {

		void Awake() {
			
		}

		internal override void InitState(BuildingController _controller) {
            if(!_controller.IsMine()) {
                return;
            }
            
            this.controller = _controller;

            for(int i = 0; i < (int)BuildingActionType.End; ++i) {
				this.actionList.Add(null);
			}

            for(int i = 0; i < (int)BuildingActionType.End; ++i) {
                switch((BuildingActionType)i) {
                    case BuildingActionType.Harvest:
                        this.actionList[i] = new Harvest(this);
                    break;
                    default:
                    continue;
                }
            }

            StartAllActions();
        }
	}
}