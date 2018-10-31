using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Building {
	public class Produce : BuildingAction {
		private static List<Vector2> producePosList = new List<Vector2>(16);

		private int producePosIdx;
		private Coroutine produceCoroutine;
        
		internal Produce(BuildingStateController _stateController)
		 : base(BuildingActionType.Produce, _stateController) {
			 if(producePosList.Count == 0) {
				CreateProducePos();
			}
			
		}

		private void CreateProducePos() {
			//Rect Bottom
			producePosList.Add(new Vector2(-32.0f, -64.0f));
			producePosList.Add(new Vector2(0.0f, -64.0f));
			producePosList.Add(new Vector2(32.0f, -64.0f));
			producePosList.Add(new Vector2(64.0f, -64.0f));
			//Rect Right
			producePosList.Add(new Vector2(64.0f, -32.0f));
			producePosList.Add(new Vector2(64.0f, 0.0f));
			producePosList.Add(new Vector2(64.0f, 32.0f));
			producePosList.Add(new Vector2(64.0f, 64.0f));
			//Rect Up
			producePosList.Add(new Vector2(32.0f, 64.0f));
			producePosList.Add(new Vector2(0.0f, 64.0f));
			producePosList.Add(new Vector2(-32.0f, 64.0f));
			producePosList.Add(new Vector2(-64.0f, 64.0f));
			//Rect Left
			producePosList.Add(new Vector2(-64.0f, 32.0f));
			producePosList.Add(new Vector2(-64.0f, 0.0f));
			producePosList.Add(new Vector2(-64.0f, -32.0f));
			producePosList.Add(new Vector2(-64.0f, -64.0f));
		}

		//override
		internal override void StartAction() {
			this.produceCoroutine = this.controller.StartCoroutine(ProduceAction());
        }
		internal override void StopAction() {
            this.controller.StopCoroutine(this.produceCoroutine);
        }

		//coroutine
		IEnumerator ProduceAction() {
			yield return new WaitUntil(() => PhotonNetwork.connectionStateDetailed == ClientState.Joined);
			
			while(true) {
				yield return new WaitForSecondsRealtime(this.controller.Status.produceCompleteTime);
				Vector2 producePos = producePosList[producePosIdx] + (Vector2)this.controller.transform.position;
				producePosIdx++;
				if(producePosIdx >= producePosList.Count) {
					producePosIdx = 0;
				}
				this.controller.PieceManager.CreateUnit((Piece.PieceType)this.controller.Status.producePieceId, producePos);
			}
		}
	}
}