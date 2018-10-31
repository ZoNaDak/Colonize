using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Building {
	public enum BuildingActionType {
		Produce,
        Harvest,
		End
	}

	public abstract class BuildingAction : UnitAction<BuildingActionType, BuildingController> {
		protected readonly BuildingController controller;
		protected readonly BuildingStateController stateController;

		internal BuildingAction(BuildingActionType _type, BuildingStateController _stateController)
		 : base(_type) {
			this.stateController = _stateController;
			this.controller = this.stateController.Controller;
		}

		internal override abstract void StartAction();
		internal override abstract void StopAction();
	}
}