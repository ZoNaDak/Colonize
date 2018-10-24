using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattern.Factory {
	public class PrefabFactory : Singleton.Singleton<PrefabFactory> {
		private Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();
		private string strLoadErr = "Error : Can't Load Prefab. Category : {0}, Prefab : {1}";

		public GameObject CreatePrefab(string _category, string _prefabName, bool _save) {
			if(this.prefabDictionary.ContainsKey(_prefabName)) {
				Debug.Log("Already Saved Prefab");
				return prefabDictionary[_prefabName];
			}

			GameObject prefab = Resources.Load(System.IO.Path.Combine("Prefabs", _category, _prefabName)) as GameObject;
			if(prefab == null){
				Debug.LogError(string.Format(strLoadErr, _category, _prefabName));
				return null;
			}
			if(_save) {
				this.prefabDictionary.Add(_prefabName, prefab);
			}

			return prefab;
		}

		public GameObject FindPrefab(string _category, string _prefabName) {
			if(!this.prefabDictionary.ContainsKey(_prefabName)) {
				throw new System.ArgumentOutOfRangeException("Can't Find Prefab");
			}
			return this.prefabDictionary[_prefabName];
		}

		public void AllClearDictionary() {
			prefabDictionary.Clear();
		}
	}
}
