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
		protected List<string> unitSpriteNames = new List<string>();
		protected int playerId = -1;

		internal Dictionary<TType, TStatus> UnitInfoDictionary { get { return unitInfoDictionary; } }
		internal List<string> UnitSpriteNames { get { return unitSpriteNames; } }

		public List<TController> UnitList { get { return unitList; } }
		public int PlayerId { get { return playerId; } }

		protected IEnumerator StartManager(int _playerId, string _xmlHeadName, string _xmlNodeName) {
			yield return new WaitUntil(() => { return DefaultManager.GameController.Instance.Ready; });
			this.playerId = _playerId;
			yield return ControllUI.UnitControll.UnitControllBar.Instance.WaitForReady();
			
			try {
				XmlNodeList xmlNodes = MyXml.XmlManager.LoadXmlNodes(_xmlHeadName, _xmlNodeName);
				SaveUnitInfoWithCoroutine(xmlNodes, _xmlHeadName);
				this.unitSpriteNames.Add("BP_{0}");
				this.unitSpriteNames.Add("WP_{0}");
			} catch (System.NullReferenceException ex) {
				throw ex;
			}				
		}

		public IEnumerable<TController> GetAllUnits() {
			return from controller in this.unitList
					select controller;
		}

		public void AddUnit(TController _controller) {
			if(_controller != null) {
				this.unitList.Add(_controller);
			} else {
				throw new System.ArgumentNullException("UnitController is null!");
			}
		}

		public bool RemoveUnit(TController _controller) {
			try {
				unitList.Remove(_controller);
				Destroy(_controller.gameObject, 0.1f);
				_controller.gameObject.SetActive(false);
			} catch(System.ArgumentNullException ex) {
				throw ex;
			} catch(System.NullReferenceException ex) {
				throw ex;
			} catch(System.Exception ex) {
				throw ex;
			}

			return true;
		}

		//abstract
		public abstract TController CreateUnit(TType _type, Vector2 _pos);
		public abstract IEnumerable<TController> GetUnits(TType _type);
		protected abstract void SaveUnitInfoWithCoroutine(XmlNodeList _xmlNodes, string _xmlName);
	}
}