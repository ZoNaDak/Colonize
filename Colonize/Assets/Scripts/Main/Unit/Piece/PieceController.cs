﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public sealed class PieceController : UnitController<PieceController, PieceStatus, PieceType> {		
		private PieceStateController stateController;

		static private Piece.PieceManager pieceManager;

		void Awake() {
			stateController = new PieceStateController(this);
			stateController.InitState();
			if(!this.photonView.isMine) {
				this.GetComponent<Rigidbody2D>().isKinematic = true;
			}
		}
		
		void Start () {

		}

		void Update () {
			
		}

		internal static void SetPieceManager(Piece.PieceManager _pieceManager) {
			pieceManager = _pieceManager;
		}

		public void SetMoveState(List<Vector2> _movePosList) {
			this.stateController.SetMovePosList(_movePosList);
			this.stateController.ChangeState(PieceStateType.Move);
		}

		//Photon
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if(stream.isWriting) {
				stream.SendNext(this.transform.position);
			} else if(stream.isReading) {
				this.transform.position = (Vector3)stream.ReceiveNext();
			}
		}

		public override void SetData(int _playerId, PieceType _type) {
			SetDataOnPhoton(_playerId, _type);
			this.photonView.RPC("SetDataOnPhoton", PhotonTargets.Others, _playerId, _type);
			unitNum++;
			this.Notify();
		}

		[PunRPC]
		protected override void SetDataOnPhoton(int _playerId, PieceType _type){
			this.playerId = _playerId;
			this.status = pieceManager.UnitInfoDictionary[_type];
			this.spriteRenderer.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(pieceManager.PieceSpriteNames[this.playerId], this.status.name));
		}
	}
}