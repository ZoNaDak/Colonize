﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Unit;
using Colonize.Unit.Building;
using Colonize.Unit.Piece;
using Pattern.Factory;

namespace Colonize.Player {
	public class PlayerController : MonoBehaviour {
		private static string goldTextFormat = "Gold : {0}";

		private BuildingManager buildingManager;
		private PieceManager pieceManager;
		private int gold;
		private int playerID;

		public UnityEngine.UI.Text goldText { get; set; }

		public int PlayerId { get { return playerID; }}
		public BuildingManager BuildingManager { get { return buildingManager; } }
		public PieceManager PieceManager { get { return pieceManager; } }

		public int Gold {
			get { return this.gold; }
			set {
				this.gold = value;
				goldText.text = string.Format(goldTextFormat, this.gold.ToString());
			}
		}

		// Use this for initialization
		void Start () {
			if(this.playerID == DefaultManager.GameController.Instance.PlayerId) {
				this.Gold = 0;
			}
		}

		// Update is called once per frame
		void Update () {

		}

		public void Intialize(int _id) {
			this.playerID = _id;
			GameObject Prefab;
			//BuildingManager
			Prefab = Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Player", "BuildingManager", false);
			this.buildingManager = Instantiate(Prefab, this.transform.position, Quaternion.identity, this.transform).GetComponent<BuildingManager>();
			//PieceManager
			Prefab = Pattern.Factory.PrefabFactory.Instance.CreatePrefab("Player", "PieceManager", false);
			this.pieceManager = Instantiate(Prefab, this.transform.position, Quaternion.identity, this.transform).GetComponent<PieceManager>();
		}

		public void CreateForGameStart(Vector2 _pos) {
			this.buildingManager.CreateUnit(BuildingType.Commander, _pos);
		}

		public bool CheckLose() {
			if(this.buildingManager.UnitList.Count == 0) {
				return true;
			}
			return false;
		}
	}
}