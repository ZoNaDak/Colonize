﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.ControllUI.ControllBoard.MiniUnit;

namespace Colonize.Unit.Building {
	public sealed class BuildingController : UnitController<BuildingController, BuildingStatus, BuildingType> {
		
		private static List<Vector2> producePosList = new List<Vector2>(16);

		private int producePosIdx;

		static private Building.BuildingManager buildingManager;
		static private Piece.PieceManager pieceManager;

		void Awake() {
			if(producePosList.Count == 0) {
				CreateProducePos();
			}
		}

		void Start () {
			if(this.photonView.isMine) {
				StartCoroutine(CreatingUnit());
			}
		}

		void Update () {
			
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

		internal static void SetBuildingManager(BuildingManager _buildingManager) {
			buildingManager = _buildingManager;
		}

		internal static void SetPieceManager(Piece.PieceManager _pieceManager) {
			pieceManager = _pieceManager;
		}

		//override
		public override void OnDestroy() {
			this.ClearObservers();
			unitNum = 0;
		}

		public override int Damaged(int _damage) {
			DamagedOnPhoton(_damage);
			this.photonView.RPC("DamagedOnPhoton", PhotonTargets.Others, _damage);
			return this.status.hp;
		}

		public override void SetData(int _playerId, BuildingType _type) {
			SetDataOnPhoton(_playerId, _type);
			this.photonView.RPC("SetDataOnPhoton", PhotonTargets.Others, _playerId, _type);
		}

		//Coroutine
		private IEnumerator CreatingUnit() {
			yield return new WaitUntil(() => PhotonNetwork.connectionStateDetailed == ClientState.Joined);
			
			while(true) {
				yield return new WaitForSecondsRealtime(this.status.produceCompleteTime);
				Vector2 producePos = producePosList[producePosIdx] + (Vector2)this.transform.position;
				producePosIdx++;
				if(producePosIdx >= producePosList.Count) {
					producePosIdx = 0;
				}
				pieceManager.CreateUnit(Piece.PieceType.SwordMan, producePos);
			}
		}

		//Photon
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		}

		[PunRPC]
		protected override void SetDataOnPhoton(int _playerId, BuildingType _type) {
			this.playerId = _playerId;
			this.status = buildingManager.UnitInfoDictionary[_type];
			this.spriteRenderer.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(buildingManager.UnitSpriteNames[this.playerId], this.status.name));
			MiniUnitManager.Instance.CreateMiniUnit(this);
		}

		[PunRPC]
		protected override void DamagedOnPhoton(int _damage) {
			this.status.hp -= _damage;
			if(this.status.hp <= 0) {
				this.dead = true;
				buildingManager.RemoveUnit(this);
			}
		}
	}
}

