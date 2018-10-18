using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Colonize.Unit.Piece {
	[Serializable]
	public enum PieceType {
		SwordMan,
		End
	}

	[Serializable]
	public struct PieceStatus {
		public readonly PieceType type;
		public readonly string name;
		public readonly int hp;
		public readonly int attack;
		public readonly int speed;

		public PieceStatus(PieceType _type, string _name, int _hp, int _attack, int _speed) {
			this.type = _type;
			this.name = _name;
			this.hp = _hp;
			this.attack = _attack;
			this.speed = _speed;
		}

		//Serialize Function
		[NonSerialized] private static MemoryStream stream = new MemoryStream();
		[NonSerialized] private static BinaryFormatter formatter = new BinaryFormatter();
		
		public static byte[] Serialize(object _status) {
			PieceStatus status = (PieceStatus)_status;
			formatter.Serialize(stream, status);
			return stream.GetBuffer();
		}

		public static object Deserialize(byte[] data) {
			stream.Write(data, 0, data.Length);
			stream.Position = 0;
			return (PieceStatus)formatter.Deserialize(stream);
		}
	}
}

