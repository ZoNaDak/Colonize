using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine;
using Colonize.ControllUI.UnitControll;

namespace Colonize.Unit.Building {
	public sealed class BuildingManager : UnitManager<BuildingManager, BuildingController, BuildingStatus, BuildingType> {
		private Player.PlayerController player;

		internal Player.PlayerController Player { get { return player; } }

		void Awake () {
			
		}

		void Start () {
			this.player = this.transform.parent.GetComponentInChildren<Player.PlayerController>();
			StartCoroutine(StartManager(this.player.PlayerId, "BuildingInfo", "Building"));
		}

		void Update () {

		}

		public bool CheckIsBuildingInLand(Vector2Int _landIdx) {
			for(int i = 0; i < this.unitList.Count; ++i) {
				if(_landIdx == Map.MapManager.Instance.GetLandIdx(this.unitList[i].transform.position)) {
					return true;
				}
			}
			return false;
		}

		public BuildingController NearestBuilding(Vector2 _pos) {
			BuildingController nearest = this.unitList[0];
			float minDist = Vector2.Distance(_pos, this.unitList[0].transform.position);
			for(int i = 1; i < this.unitList.Count; ++i) {
				float dist = Vector2.Distance(_pos, this.unitList[i].transform.position);
				if(minDist > dist) {
					nearest = this.unitList[i];
					minDist = dist;
				}
			}

			return nearest;
		}

		//overide
		protected override void SaveUnitInfoWithCoroutine (XmlNodeList _xmlNodes, string _xmlName) {
			foreach(XmlNode node in _xmlNodes) {
				BuildingStatus status =  new BuildingStatus(
					(BuildingType)(System.Convert.ToInt32(node.SelectSingleNode("Id").InnerText)),
					node.SelectSingleNode("Name").InnerText,
					System.Convert.ToInt32(node.SelectSingleNode("Hp").InnerText),
					System.Convert.ToInt32(node.SelectSingleNode("ProducePieceId").InnerText),
					float.Parse(node.SelectSingleNode("ProduceTime").InnerText),
					System.Convert.ToInt32(node.SelectSingleNode("HarvestGold").InnerText),
					System.Convert.ToInt32(node.SelectSingleNode("Cost").InnerText));
				this.unitInfoDictionary.Add(status.type, status);
				Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Buildings", status.type.ToString(), true);
				UnitControllButton button =  UnitControllBar.Instance.FindButton(status.type);
				button.SetGold(status.cost);
			}
			MyXml.XmlManager.ClearXmlDoc(_xmlName);
		}

		public override BuildingController CreateUnit(BuildingType _type, Vector2 _pos) {
			BuildingController building;
			try {
				GameObject buildingPrefab = Pattern.Factory.PrefabFactory.Instance.FindPrefab("Buildings", _type.ToString());
				building = PhotonNetwork.Instantiate(string.Format("Prefabs/Buildings/{0}", _type.ToString())
				, new Vector3(_pos.x, _pos.y, buildingPrefab.transform.position.z)
				, Quaternion.identity, 0).GetComponent<BuildingController>();
				building.transform.SetParent(this.transform);
				building.SetBuildingManager(this);
				building.SetPieceManager(this.player.PieceManager);
				building.SetData(this.playerId, _type);
				this.unitList.Add(building);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
			return building;
		}

		public BuildingController CreateUnit(BuildingType _type, Vector2Int _landIdx) {
			BuildingController building;
			try {
				Vector2 pos = Map.MapManager.Instance.GetLandPos(_landIdx.x, _landIdx.y);
				GameObject buildingPrefab = Pattern.Factory.PrefabFactory.Instance.FindPrefab("Buildings", _type.ToString());
				building = PhotonNetwork.Instantiate(string.Format("Prefabs/Buildings/{0}", _type.ToString())
				, new Vector3(pos.x, pos.y, buildingPrefab.transform.position.z)
				, Quaternion.identity, 0).GetComponent<BuildingController>();
				building.transform.SetParent(this.transform);
				building.SetBuildingManager(this);
				building.SetPieceManager(this.player.PieceManager);
				building.SetData(this.playerId, _type);
				this.unitList.Add(building);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
			return building;
		}

		public override IEnumerable<BuildingController> GetUnits(BuildingType _type) {
			return from controller in this.unitList
					where controller.Status.type == _type
					select controller;
		}
	}
}