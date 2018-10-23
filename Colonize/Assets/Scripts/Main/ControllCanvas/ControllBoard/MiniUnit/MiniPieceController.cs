using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Unit.Piece;
using Colonize.ControllUI.ControllBoard;

namespace Colonize.ControllUI.ControllBoard.MiniUnit {
	public class MiniPieceController : MiniUnitController<PieceController, PieceStatus, PieceType> {
		
		void Start () {

		}

		
		void Update () {
			CheckDead();
		}

		void LateUpdate() {
			this.transform.localPosition = ControllBoard.Instance.GetBoardPosFromWorldPos(this.unitController.transform.position);
		}

		//override
		public override void Initialize(int _playerId, PieceController _controller) {
			if(_playerId == _controller.PlayerId) {
				this.unitImage.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Mini_Piece_B");
			} else {
				this.unitImage.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Mini_Piece_R");
			}
			this.unitController = _controller;
			this.transform.localPosition = ControllBoard.Instance.GetBoardPosFromWorldPos(_controller.transform.position);
		}

		public override void RemoveSelf() {
			Destroy(this);
		}

		public override void CheckDead() {
			if(this.unitController.Dead == true && MiniUnitManager.OnManager) {
				MiniUnitManager.Instance.DestroyMiniUnit(this.unitController as PieceController);
			}
		}
	}
}