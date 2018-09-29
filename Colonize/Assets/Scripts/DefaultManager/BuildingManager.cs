using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Building {
	public class BuildingManager : SingletonPattern.MonoSingleton<BuildingManager> {
		private Dictionary<BuildingType, BuildingStatus> buidlingInfoDictionary = new Dictionary<BuildingType, BuildingStatus>();
		private List<BuildingController> buildingList = new List<BuildingController>();
		private string pieceSpriteName;
		private int playerId = -1;

		public List<BuildingController> BuildingList { get { return buildingList; } }
		public int PlayerId { get { return playerId; } }

		void Awake () {
			try {
				XmlNodeList xmlNodes = MyXml.XmlManager.LoadXmlNodes("BuildingInfo", "Building");
				SaveBuildingInfo(xmlNodes);
			} catch (System.NullReferenceException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			} finally {
				MyXml.XmlManager.ClearXmlDoc();
			}
		}

		void Start () {
			
		}

		void Update () {

		}

		private void SaveBuildingInfo (XmlNodeList _xmlNodes) {
			foreach(XmlNode node in _xmlNodes) {
				BuildingStatus status =  new BuildingStatus(
					(BuildingType)(System.Convert.ToInt32(node.SelectSingleNode("Id").InnerText)),
					node.SelectSingleNode("Name").InnerText,
					System.Convert.ToInt32(node.SelectSingleNode("Hp").InnerText));
				this.buidlingInfoDictionary.Add(status.type, status);
			}
		}

		public void CreateBuilding(BuildingType _type, Vector2 _pos) {
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
				GameObject buildingPrefab = Prefab.PrefabFactory.Instance.CreatePrefab("Buildings", _type.ToString(), true);
				BuildingController building = Instantiate(buildingPrefab
					, new Vector3(_pos.x, _pos.y, buildingPrefab.transform.position.z)
					, Quaternion.identity
					, this.transform).GetComponent<BuildingController>();
				BuildingStatus status = this.buidlingInfoDictionary[_type];
				building.SetData(this.playerId, status
					, SpirteFactory.SpriteFactory.Instance.GetSprite("PiecesAtlas", string.Format(pieceSpriteName, status.name)));
				this.buildingList.Add(building);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}
	}
}