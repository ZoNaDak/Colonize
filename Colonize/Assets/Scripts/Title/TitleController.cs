using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Colonize.Title {
	public class TitleController : MonoBehaviour {

		void Awake() {
			#if !UNITY_EDITOR
				Debug.unityLogger.logEnabled = false;
			#endif

			//SetResoultion
			//Screen.SetResolution(720, 1280, false);
			Screen.SetResolution(360, 640, false);
		}

		void Start () {

		}

		void Update () {

		}

		public void OnStartButtonClicked() {
			SceneManager.LoadScene("Join");
			//DontDestroyOnLoad(StaticData.Instance);
		}

		public void OnQuitButtonClicked() {
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else 
			Application.Quit();
			#endif
		}
	}
}