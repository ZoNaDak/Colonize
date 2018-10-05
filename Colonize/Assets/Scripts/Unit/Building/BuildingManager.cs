using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Unit;

namespace Building {
	public sealed class BuildingManager : UnitManager<BuildingManager, BuildingController, BuildingStatus, BuildingType> {
		
		void Awake () {
			AwakeManager("BuildingInfo", "Building");
		}

		void Start () {
			
		}

		void Update () {

		}

		protected override IEnumerator SaveUnitInfoWithCoroutine (XmlNodeList _xmlNodes, string _xmlName) {
			foreach(XmlNode node in _xmlNodes) {
				BuildingStatus status =  new BuildingStatus(
					(BuildingType)(System.Convert.ToInt32(node.SelectSingleNode("Id").InnerText)),
					node.SelectSingleNode("Name").InnerText,
					System.Convert.ToInt32(node.SelectSingleNode("Hp").InnerText),
					float.Parse(node.SelectSingleNode("Produce").InnerText));
				this.unitInfoDictionary.Add(status.type, status);
				Prefab.PrefabFactory.Instance.CreatePrefab("Buildings", status.type.ToString(), true);
			}

			MyXml.XmlManager.ClearXmlDoc(_xmlName);

			yield return null;
		}

		public override void CreateUnit(BuildingType _type, Vector2 _pos) {
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
				GameObject buildingPrefab = Prefab.PrefabFactory.Instance.FindPrefab("Buildings", _type.ToString());
				BuildingController building = Instantiate(buildingPrefab
					, new Vector3(_pos.x, _pos.y, buildingPrefab.transform.position.z)
					, Quaternion.identity
					, this.transform).GetComponent<BuildingController>();
				BuildingStatus status = this.unitInfoDictionary[_type];
				building.SetData(this.playerId, status
					, SpirteFactory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(pieceSpriteName, status.name)));
				this.unitList.Add(building);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}
	}
}