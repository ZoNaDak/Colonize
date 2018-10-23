using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Unit.Building;
using Colonize.Unit.Piece;

namespace Colonize.ControllUI.ControllBoard.MiniUnit {
	public class MiniUnitManager : Pattern.Singleton.MonoSingleton<MiniUnitManager> {
		private Dictionary<BuildingController, MiniBuildingController> miniBuildingDictionary = new Dictionary<BuildingController, MiniBuildingController>();
		private Dictionary<PieceController, MiniPieceController> miniPieceDictionary = new Dictionary<PieceController, MiniPieceController>();

		private static bool onManager;

		public static bool OnManager { get { return onManager; } }
		
		void Awake() {
			Pattern.Factory.PrefabFactory.Instance.CreatePrefab("MiniUnits", "MiniBuilding", true);
			Pattern.Factory.PrefabFactory.Instance.CreatePrefab("MiniUnits", "MiniPiece", true);
			onManager = true;
		}

		void Start () {

		}

		void Update () {

		}

		void OnDestroy() {
			onManager = false;
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

		public void CreateMiniUnit(PieceController _pieceController) {
			try {
				GameObject miniPiecePrefab = Pattern.Factory.PrefabFactory.Instance.FindPrefab("MiniUnits", "MiniPiece");
				MiniPieceController miniPiece = Instantiate(miniPiecePrefab, this.transform).GetComponent<MiniPieceController>();
				miniPiece.Initialize(DefaultManager.GameController.Instance.PlayerId, _pieceController);
				this.miniPieceDictionary.Add(_pieceController, miniPiece);
			} catch (System.NullReferenceException ex) {
				throw ex;
			} catch (System.ArgumentNullException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
		}

		public void DestroyMiniUnit(BuildingController _buildingController) {
			try {
				if(!miniBuildingDictionary.ContainsKey(_buildingController)) {
					throw new System.NullReferenceException("Can't Find MiniBuidling");
				}
				Destroy(miniBuildingDictionary[_buildingController].gameObject);
				miniBuildingDictionary.Remove(_buildingController);
			} catch (System.ArgumentNullException ex) {
				throw ex;
			} catch (System.NullReferenceException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
		}

		public void DestroyMiniUnit(PieceController _pieceController) {
			try {
				if(!miniPieceDictionary.ContainsKey(_pieceController)) {
					throw new System.NullReferenceException("Can't Find MiniBuidling");
				}
				Destroy(miniPieceDictionary[_pieceController].gameObject);
				miniPieceDictionary.Remove(_pieceController);
			} catch (System.ArgumentNullException ex) {
				throw ex;
			} catch (System.NullReferenceException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
		}
	}
}

