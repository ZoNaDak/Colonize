using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map {
	public class MapManager : Pattern.Singleton.MonoSingleton<MapManager> {
		private List<LandController> landList = new List<LandController>(landNum);
		private const int landNum = 25;
		private const int landX_Num = 5;

		public int LandNum { get {return landNum; } }
		public int LandX_Num { get {return landX_Num; } }

		void Awake() {
			try {
				GameObject landPrefab = Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Lands", "BasicLand", false);
				float landSize = landPrefab.transform.localScale.x;
				this.transform.Translate(landSize * 0.5f, landSize * 0.5f, 0.0f);
				for(int i = 0; i < landNum; ++i) {
					LandController land = Instantiate(landPrefab
						, new Vector3(
							this.transform.position.x + landSize * (i%landX_Num)
							, this.transform.position.y + landSize * (i/landX_Num)
							, this.transform.position.z)
						, Quaternion.identity
						, this.transform).GetComponent<LandController>();
					this.landList.Add(land);
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

		public Vector3 GetLandPos(int _landIdx_X, int _landIdx_Y) {
			return this.landList[_landIdx_X + _landIdx_Y * landX_Num].transform.position;
		}

		public Vector2 GetLandSize() {
			return this.landList[0].transform.localScale;
		}
	}
}