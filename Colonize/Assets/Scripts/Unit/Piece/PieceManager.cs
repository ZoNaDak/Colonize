using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Colonize.Unit.Piece {
	public sealed class PieceManager : UnitManager<PieceManager, PieceController, PieceStatus, PieceType> {
		
		void Awake() {
			AwakeManager("PieceInfo", "Piece");
		}

		void Start () {
			
		}

		void Update () {

		}

		protected override IEnumerator SaveUnitInfoWithCoroutine (XmlNodeList _xmlNodes, string _xmlName) {
			yield return ControllUI.UnitControll.UnitControllBar.Instance.WaitForReady();

			foreach(XmlNode node in _xmlNodes) {
				PieceStatus status =  new PieceStatus(
					(PieceType)(System.Convert.ToInt32(node.SelectSingleNode("Id").InnerText)),
					node.SelectSingleNode("Name").InnerText,
					System.Convert.ToInt32(node.SelectSingleNode("Hp").InnerText),
					System.Convert.ToInt32(node.SelectSingleNode("Attack").InnerText),
					System.Convert.ToInt32(node.SelectSingleNode("Speed").InnerText));
				this.unitInfoDictionary.Add(status.type, status);
				GameObject piecePrefab = Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Pieces", status.type.ToString(), true);
				piecePrefab.GetComponent<PieceController>().AddObserver(ControllUI.UnitControll.UnitControllBar.Instance.FindButton(status.type.ToString()));
			}

			MyXml.XmlManager.ClearXmlDoc(_xmlName);

			yield return true;
		}

		public override void CreateUnit(PieceType _type, Vector2 _pos) {
			if(this.playerId == -1) {
				this.playerId = DefaultManager.GameController.Instance.PlayerId;
				switch(this.playerId) {
					case 0:
						this.pieceSpriteName = "BP_{0}";
					break;
					case 1:
						this.pieceSpriteName = "WP_{0}";
					break;
					default:
						throw new System.ArgumentOutOfRangeException("Player Id is Not Correct!");
				}
			}

			try {
				GameObject piecePrefab = Pattern.Factory.PrefabFactory.Instance.FindPrefab("Pieces", _type.ToString());
				PieceController piece = Instantiate(piecePrefab
					, new Vector3(_pos.x, _pos.y, piecePrefab.transform.position.z)
					, Quaternion.identity
					, this.transform).GetComponent<PieceController>();
				PieceStatus status = this.unitInfoDictionary[_type];
				piece.SetData(this.playerId, status
					, Pattern.Factory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(pieceSpriteName, status.name)));
				this.unitList.Add(piece);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		public void MovePieces(PieceType _type) {
			var pieces = GetUnits(_type);
			for(int i = 0; i < pieces.Count; ++i) {
				Debug.Log("Move");
			}
		}
	}
}

