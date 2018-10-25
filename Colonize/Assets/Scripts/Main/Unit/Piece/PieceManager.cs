using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public sealed class PieceManager : UnitManager<PieceManager, PieceController, PieceStatus, PieceType> {
		private Player.PlayerController player;

		void Awake() {
			
		}

		void Start () {
			this.player = this.transform.parent.GetComponentInChildren<Player.PlayerController>();
			StartCoroutine(StartManager(this.player.PlayerId, "PieceInfo", "Piece"));
		}

		void Update () {

		}

		private IEnumerable<IGrouping<Vector2Int, PieceController>> GetUnitsGroupByLand(PieceType _type) {
			var selectedUnits =  from controller in this.unitList group controller by Map.MapManager.Instance.GetLandIdx(controller.transform.position);
			return selectedUnits;
		}

		public void MovePieces(PieceType _type, Vector2Int _destLandPos) {
			var selectedPieces = GetUnitsGroupByLand(_type);
			foreach(var piecesInSameLand in selectedPieces) {
				List<Vector2> path = Map.MapManager.Instance.FindPath(piecesInSameLand.Key, _destLandPos);
				if(path.Count == 0) {
					path.Add(Map.MapManager.Instance.GetLandPos(piecesInSameLand.Key.x, piecesInSameLand.Key.y));
				}
				foreach(var piece in piecesInSameLand) {
					piece.SetMoveState(path);
				}
			}
		}

		public void AttackPieces(PieceType _type, Vector2Int _destLandPos) {
			var selectedPieces = GetUnitsGroupByLand(_type);
			foreach(var piecesInSameLand in selectedPieces) {
				List<Vector2> path = Map.MapManager.Instance.FindPath(piecesInSameLand.Key, _destLandPos);
				foreach(var piece in piecesInSameLand) {
					piece.SetAttackState(path);
				}
			}
		}

		//overide
		protected override void SaveUnitInfoWithCoroutine (XmlNodeList _xmlNodes, string _xmlName) {
			foreach(XmlNode node in _xmlNodes) {
				PieceStatus status =  new PieceStatus(
					(PieceType)(System.Convert.ToInt32(node.SelectSingleNode("Id").InnerText)),
					node.SelectSingleNode("Name").InnerText,
					System.Convert.ToInt32(node.SelectSingleNode("Hp").InnerText),
					System.Convert.ToInt32(node.SelectSingleNode("Attack").InnerText),
					float.Parse(node.SelectSingleNode("Speed").InnerText),
					float.Parse(node.SelectSingleNode("VisualRange").InnerText),
					float.Parse(node.SelectSingleNode("AttackRange").InnerText),
					float.Parse(node.SelectSingleNode("AttackCooltime").InnerText));
				this.unitInfoDictionary.Add(status.type, status);
				GameObject piecePrefab = Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Pieces", status.type.ToString(), true);
				piecePrefab.GetComponent<PieceController>().AddObserver(ControllUI.UnitControll.UnitControllBar.Instance.FindButton(status.type.ToString()));
			}

			MyXml.XmlManager.ClearXmlDoc(_xmlName);
		}

		public override void CreateUnit(PieceType _type, Vector2 _pos) {
			try {
				GameObject piecePrefab = Pattern.Factory.PrefabFactory.Instance.FindPrefab("Pieces", _type.ToString());
				PieceController piece = PhotonNetwork.Instantiate(string.Format("Prefabs/Pieces/{0}", _type.ToString())
					, new Vector3(_pos.x, _pos.y, piecePrefab.transform.position.z)
					, Quaternion.identity, 0).GetComponent<PieceController>();
				piece.transform.SetParent(this.transform);
				piece.SetPieceManager(this);
				piece.SetData(this.playerId, _type);
				this.unitList.Add(piece);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		public override IEnumerable<PieceController> GetUnits(PieceType _type) {
			return from controller in this.unitList
					where controller.Status.type == _type
					select controller;
		}
	}
}