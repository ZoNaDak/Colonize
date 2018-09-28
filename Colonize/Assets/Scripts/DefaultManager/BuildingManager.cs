using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building {
	public class BuildingManager : SingletonPattern.MonoSingleton<BuildingManager> {
		private List<BuildingController> buildingList = new List<BuildingController>();

		public List<BuildingController> BuildingList { get { return buildingList; } }

		void Start () {

		}

		void Update () {

		}

		public void CreateBuilding(BuildingType _type, Vector2 _pos) {	
			try {
				GameObject buildingPrefab = Prefab.PrefabFactory.Instance.CreatePrefab("Buildings", _type.ToString(), true);
				BuildingController building = Instantiate(buildingPrefab
					, new Vector3(_pos.x, _pos.y, buildingPrefab.transform.position.z)
					, Quaternion.identity
					, this.transform).GetComponent<BuildingController>();
				this.buildingList.Add(building);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}
	}
}