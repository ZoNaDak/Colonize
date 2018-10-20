using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Unit.Building;
using Colonize.ControllUI.ControllBoard;

namespace Colonize.ControllUI.ControllBoard.MiniUnit {
	public class MiniBuildingController : MiniUnitController<BuildingController, BuildingStatus, BuildingType> {
		
		void Start () {

		}

		
		void Update () {

		}

		//override
		public override void Initialize(int _playerId, BuildingController _controller) {
			if(_playerId == _controller.PlayerId) {
				this.unitImage.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Mini_Building_B");
			} else {
				this.unitImage.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("ControllUIAtlas", "Mini_Building_R");
			}
			this.unitController = _controller;
			this.transform.localPosition = ControllBoard.Instance.GetBoardPosFromWorldPos(_controller.transform.position);
		}

		public override void RemoveSelf() {
			Destroy(this);
		}
	}
}

