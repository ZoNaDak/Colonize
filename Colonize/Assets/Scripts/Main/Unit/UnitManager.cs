using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class UnitManager<T, TController, TStatus, TType> : Pattern.Singleton.MonoSingleton<UnitManager<T, TController, TStatus, TType>>
		where T : class
		where TController : UnitController<TController, TStatus>
		where TStatus : struct
		where TType : struct, IComparable, IFormattable, IConvertible {

		protected Dictionary<TType, TStatus> unitInfoDictionary = new Dictionary<TType, TStatus>();
		protected List<TController> unitList = new List<TController>();
		protected string pieceSpriteName;
		protected int playerId = -1;

		public List<TController> UnitList { get { return unitList; } }
		public int PlayerId { get { return playerId; } }

		protected abstract IEnumerator SaveUnitInfoWithCoroutine(XmlNodeList _xmlNodes, string _xmlName);
		public abstract void CreateUnit(TType _type, Vector2 _pos);

		protected void AwakeManager(string _xmlHeadName, string _xmlNodeName) {
			try {
				XmlNodeList xmlNodes = MyXml.XmlManager.LoadXmlNodes(_xmlHeadName, _xmlNodeName);
				StartCoroutine(SaveUnitInfoWithCoroutine(xmlNodes, _xmlHeadName));
			} catch (System.NullReferenceException ex) {
				throw ex;
			} catch (System.Exception ex) {
				throw ex;
			}
		}

		public IEnumerable<TController> GetAllUnits() {
			return from controller in this.unitList
					select controller;
		}

		public abstract IEnumerable<TController> GetUnits(TType _type);
	}
}