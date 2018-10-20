﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine;

namespace Colonize.Unit.Building {
	public sealed class BuildingManager : UnitManager<BuildingManager, BuildingController, BuildingStatus, BuildingType> {

		[SerializeField] private DefaultManager.GameController gameController;

		void Awake () {
			BuildingController.SetBuildingManager(this);
		}

		void Start () {
			StartCoroutine(StartManager("BuildingInfo", "Building"));
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
				Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Buildings", status.type.ToString(), true);
			}

			MyXml.XmlManager.ClearXmlDoc(_xmlName);

			yield return null;
		}

		public override void CreateUnit(BuildingType _type, Vector2 _pos) {
			try {
				GameObject buildingPrefab = Pattern.Factory.PrefabFactory.Instance.FindPrefab("Buildings", _type.ToString());
				BuildingController building = PhotonNetwork.Instantiate(string.Format("Prefabs/Buildings/{0}", _type.ToString())
				, new Vector3(_pos.x, _pos.y, buildingPrefab.transform.position.z)
				, Quaternion.identity, 0).GetComponent<BuildingController>();
				building.transform.SetParent(this.transform);
				BuildingStatus status = this.unitInfoDictionary[_type];
				building.SetData(this.playerId, _type);
				this.unitList.Add(building);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		public override IEnumerable<BuildingController> GetUnits(BuildingType _type) {
			return from controller in this.unitList
					where controller.Status.type == _type
					select controller;
		}
	}
}