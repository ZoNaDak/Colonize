using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace SpirteFactory {
	public class SpriteFactory : SingletonPattern.Singleton<SpriteFactory> {
		public Dictionary<string, SpriteAtlas> atlasDictionary = new Dictionary<string, SpriteAtlas>();
		private string strLoadErr = "Error : Can't Load atlas. AtlasName : {0}";

		public Sprite GetSprite(string _atlasName, string _spriteName) {
			if(!this.atlasDictionary.ContainsKey(_atlasName)) {
				SpriteAtlas atlas = Resources.Load<SpriteAtlas>(System.IO.Path.Combine("Atlas", _atlasName));
				if(atlas == null) {
					Debug.LogError(string.Format(strLoadErr, _atlasName));
					return null;
				} else {
					this.atlasDictionary.Add(_atlasName, atlas);
				}
			}

			try{
				return this.atlasDictionary[_atlasName].GetSprite(_spriteName);
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}
		}
	}
}
