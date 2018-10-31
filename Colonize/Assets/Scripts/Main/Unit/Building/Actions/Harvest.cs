using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.DefaultManager;

namespace Colonize.Unit.Building {
	public class Harvest : BuildingAction {
		private static float harvestTime = 10.0f;

		private Coroutine harvestCoroutine;
        
		internal Harvest(BuildingStateController _stateController)
		 : base(BuildingActionType.Produce, _stateController) {
			
		}

		//override
		internal override void StartAction() {
			this.harvestCoroutine = this.controller.StartCoroutine(HarvestAction());
        }
		internal override void StopAction() {
            this.controller.StopCoroutine(this.harvestCoroutine);
        }

		//coroutine
		IEnumerator HarvestAction() {
			yield return new WaitUntil(() => PhotonNetwork.connectionStateDetailed == ClientState.Joined);
			
			while(true) {
				yield return new WaitForSecondsRealtime(harvestTime);
				GameController.Instance.MyPlayer.Gold += this.controller.Status.harvestGold;
			}
		}
	}
}