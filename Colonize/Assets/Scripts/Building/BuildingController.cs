using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building {
	public enum BuildingType {
		Commander,
		End
	}

	public class BuildingController : MonoBehaviour {
		private BuildingType type;

		[SerializeField] private SpriteRenderer sprite;
		
		void Start () {

		}

		void Update () {

		}

		public void SetData() {
		}
	}
}

