using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Unit.Building;
using Colonize.Unit.Piece;

namespace Colonize.ControllUI.ControllBoard.MiniUnit {
	public class MiniUnitManager : Pattern.Singleton.MonoSingleton<MiniUnitManager> {
		
		private Dictionary<BuildingController, MiniBuildingController> miniBuildingDictionary = new Dictionary<BuildingController, MiniBuildingController>();
		//private Dictionary<PieceController, MiniPieceController> miniPieceDictionary = new Dictionary<PieceController, MiniUnitController>();
		
		void Awake() {
			Pattern.Factory.PrefabFactory.Instance.CreatePrefab("MiniUnits", "MiniBuilding", true);
		}

		void Start () {

		}

		void Update () {

		}

		public void CreateMiniUnit(BuildingController _buildingController) {
			try {
				GameObject miniBuildingPrefab = Pattern.Factory.PrefabFactory.Instance.FindPrefab("MiniUnits", "MiniBuilding");
				MiniBuildingController miniBuiling = Instantiate(miniBuildingPrefab, this.transform).GetComponent<MiniBuildingController>();
				miniBuiling.Initialize(DefaultManager.GameController.Instance.PlayerId, _buildingController);
				this.miniBuildingDictionary.Add(_buildingController, miniBuiling);
			} catch (System.NullReferenceException ex) {
				throw ex;
			} catch (System.ArgumentNullException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
		}
	}
}

