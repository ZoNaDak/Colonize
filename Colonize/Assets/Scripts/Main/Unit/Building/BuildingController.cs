using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit.Building {
	public sealed class BuildingController : UnitController<BuildingController, BuildingStatus> {
		
		private static List<Vector2> producePosList = new List<Vector2>(16);

		private int producePosIdx;

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

		[PunRPC]
		protected override void SetDataOnPhoton(int _playerId, BuildingStatus _status, string _spriteName) {
			this.playerId = _playerId;
			this.status = _status;
			this.spriteRenderer.sprite = Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(_spriteName, _status.name));
		}

		internal static void SetPieceManager(Piece.PieceManager _pieceManager) {
			pieceManager = _pieceManager;
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

		public override void SetData(int _playerId, BuildingStatus _status, string _spriteName) {
			this.photonView.RPC("SetDataOnPhoton", PhotonTargets.AllBuffered, _playerId, _status, _spriteName);
		}
	}
}

