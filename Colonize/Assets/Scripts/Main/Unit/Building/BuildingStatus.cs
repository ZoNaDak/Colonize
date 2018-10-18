using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Colonize.Unit.Building {
	[Serializable]
	public enum BuildingType {
		Commander,
		End
	}

	[Serializable]
	public struct BuildingStatus {
		public readonly BuildingType type;
		public readonly string name;
		public readonly int hp;
		public readonly float produceCompleteTime;

		public BuildingStatus(BuildingType _type, string _name, int _hp, float _produceTime) {
			this.type = _type;
			this.name = _name;
			this.hp = _hp;
			this.produceCompleteTime = _produceTime;
		}

		//Serialize Function
		[NonSerialized] private static MemoryStream stream = new MemoryStream();
		[NonSerialized] private static BinaryFormatter formatter = new BinaryFormatter();
		
		public static byte[] Serialize(object _status) {
			BuildingStatus status = (BuildingStatus)_status;
			formatter.Serialize(stream, status);
			return stream.GetBuffer();
		}

		public static object Deserialize(byte[] data) {
			stream.Write(data, 0, data.Length);
			stream.Position = 0;
			return (BuildingStatus)formatter.Deserialize(stream);
		}
	}
}