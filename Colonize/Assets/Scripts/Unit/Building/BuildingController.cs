using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;

namespace Building {
	public sealed class BuildingController : UnitController<BuildingController, BuildingStatus> {
		
		private static List<Vector2> producePosList = new List<Vector2>(16);

		private float pieceProduceTime;
		private int producePosIdx;

		void Awake() {
			if(producePosList.Count == 0) {
				CreateProducePos();
			}
		}

		void Start () {

		}

		void Update () {
			this.pieceProduceTime += Time.deltaTime;
			if(this.pieceProduceTime >= this.status.produceCompleteTime) {
				this.pieceProduceTime = 0.0f;
				Vector2 producePos = producePosList[producePosIdx] + (Vector2)this.transform.position;
				producePosIdx++;
				if(producePosIdx >= producePosList.Count) {
					producePosIdx = 0;
				}
				Piece.PieceManager.Instance.CreateUnit(Piece.PieceType.SwordMan, producePos);
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

		public override void SetData(int _playerId, BuildingStatus _status, Sprite _sprite) {
			this.playerId = _playerId;
			this.status = _status;
			this.spriteRenderer.sprite = _sprite;
		}
	}
}

