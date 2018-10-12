using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Map.Land;
using Colonize.Utility.Algorithm.AStar;
using ExtensionMethod;

namespace Colonize.Map {
	public class MapManager : Pattern.Singleton.MonoSingleton<MapManager> {
		private List<List<LandController>> landList = new List<List<LandController>>(landNum);
		private const int landNum = 25;
		private const int landNumX = 5;
		private const int landNumY = 5;

		private float landSizeX;
		private float landSizeY;

		public int LandNum { get { return landNum; } }
		public int LandNumX { get { return landNumX; } }
		public int LandNumY { get { return landNumY; } }

		void Awake() {
			try {
				GameObject landPrefab = Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Lands", "BasicLand", false);
				this.landSizeX = landPrefab.transform.localScale.x;
				this.landSizeY = landPrefab.transform.localScale.y;
				//this.transform.Translate(landSize * 0.5f, landSize * 0.5f, 0.0f);
				/*for(int i = 0; i < landNum; ++i) {
					LandController land = Instantiate(landPrefab
						, new Vector3(
							this.transform.position.x + landSize * (i%landNumX)
							, this.transform.position.y + landSize * (i/landNumY)
							, this.transform.position.z)
						, Quaternion.identity
						, this.transform).GetComponent<LandController>();
					this.landList.Add(land);
				}*/
				for(int x = 0; x < landNumX; ++x) {
					this.landList.Add(new List<LandController>());
					for(int y = 0; y < landNumY; ++ y) {
						LandController land = Instantiate(landPrefab
							, new Vector3(
								this.transform.position.x + landSizeX * x
								, this.transform.position.y + landSizeY * y
								, this.transform.position.z)
							, Quaternion.identity
							, this.transform).GetComponent<LandController>();
						this.landList[x].Add(land);
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

		private List<Vector2> ChangeNodeListToPath(List<Node> _nodeList) {
			List<Vector2> path = new List<Vector2>();

			for(int i = 0; i < _nodeList.Count; ++i) {
				path.Add(this.landList[_nodeList[i].x][_nodeList[i].y].transform.position);
			}

			return path;
		}

		public Vector3 GetLandPos(int _landIdx_X, int _landIdx_Y) {
			return this.landList[_landIdx_X][_landIdx_Y].transform.position;
		}

		public Vector2 GetLandSize() {
			return this.landList[0][0].transform.localScale;
		}

		public Vector2Int GetLandIdx(Vector2 _pos) {
			Vector2 correctedPos = new Vector2(_pos.x + this.landSizeX * 0.5f, _pos.y + this.landSizeY * 0.5f);
			if(correctedPos.CheckOutRange(new Vector2(landNumX * this.landSizeX, landNumY * this.landSizeY))) {
				throw new System.ArgumentOutOfRangeException("Pos is Out Of Range");
			}
		
			return new Vector2Int((int)((_pos.x + this.landSizeX * 0.5f) / this.landSizeX), (int)((_pos.y + this.landSizeY * 0.5f) / this.landSizeY));
		}

		public List<Vector2> FindPath(Vector2Int _startLandIdx, Vector2Int _distLandIdx) {
			return ChangeNodeListToPath(AStarManager.FindPath(_startLandIdx, _distLandIdx));
		}
	}
}