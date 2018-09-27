using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map {
	public class MapManager : SingletonPattern.MonoSingleton<MapManager> {
		private List<LandController> landList = new List<LandController>(landNum);
		private const int landNum = 25;
		private const int landX_Num = 5;
		private const float posZ = 10.0f;

		void Awake() {
			try {
				GameObject landPrefab = Prefab.PrefabFactory.Instance.CreatePrefab("Lands", "BasicLand", false);
				float landSize = landPrefab.transform.localScale.x;
				if(landPrefab != null) {
					for(int i = 0; i < landNum; ++i) {
						LandController land = Instantiate(landPrefab
							, new Vector3(landSize * (i%landX_Num), landSize * (i/landX_Num) ,posZ)
							, Quaternion.identity
							, this.transform).GetComponentInChildren<LandController>();
						this.landList.Add(land);
					}
				}
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
			
		}

		void Start () {

		}

		void Update () {

		}
	}
}