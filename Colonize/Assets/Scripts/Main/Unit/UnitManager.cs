using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine;

namespace Colonize.Unit {
	public abstract class UnitManager<T, TController, TStatus, TType> : MonoBehaviour
		where T : class
		where TController : UnitController<TController, TStatus, TType>
		where TStatus : struct
		where TType : struct, IComparable, IFormattable, IConvertible {

		protected Dictionary<TType, TStatus> unitInfoDictionary = new Dictionary<TType, TStatus>();
		protected List<TController> unitList = new List<TController>();
		protected List<string> pieceSpriteNames = new List<string>();
		protected int playerId = -1;

		internal Dictionary<TType, TStatus> UnitInfoDictionary { get { return unitInfoDictionary; } }
		internal List<string> PieceSpriteNames { get { return pieceSpriteNames; } }

		public List<TController> UnitList { get { return unitList; } }
		public int PlayerId { get { return playerId; } }

		protected abstract IEnumerator SaveUnitInfoWithCoroutine(XmlNodeList _xmlNodes, string _xmlName);
		public abstract void CreateUnit(TType _type, Vector2 _pos);

		protected IEnumerator StartManager(string _xmlHeadName, string _xmlNodeName) {
			yield return new WaitUntil(() => { return DefaultManager.GameController.Instance.Ready; });
			this.playerId = DefaultManager.GameController.Instance.PlayerId;
			try {
				XmlNodeList xmlNodes = MyXml.XmlManager.LoadXmlNodes(_xmlHeadName, _xmlNodeName);
				StartCoroutine(SaveUnitInfoWithCoroutine(xmlNodes, _xmlHeadName));
				this.pieceSpriteNames.Add("BP_{0}");
				this.pieceSpriteNames.Add("WP_{0}");
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