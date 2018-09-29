using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace MyXml {
	public static class XmlManager {
		private static string strLoadErr = "Error : Can't Load Xml. name : {0}";
		private static XmlDocument xmlDoc = new XmlDocument();

		public static XmlNodeList LoadXmlNodes(string _xmlName, string _nodeName) {
			XmlNodeList loadNodes;
			TextAsset textAsset = Resources.Load(System.IO.Path.Combine("Xml", _xmlName)) as TextAsset;
			try {
				xmlDoc.LoadXml(textAsset.text);
				loadNodes = xmlDoc.SelectNodes(string.Format("{0}/{1}", _xmlName, _nodeName));
			} catch (System.NullReferenceException) {
				Debug.LogError(string.Format(strLoadErr, _xmlName));
				return null;
			} catch (System.Exception ex) {
				throw ex;
			}
			
			return loadNodes;
		}

		public static void ClearXmlDoc() {
			xmlDoc.RemoveAll();
		}
	}
}
