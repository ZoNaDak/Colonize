using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Colonize.Title {
	public class TitleController : MonoBehaviour {

		[SerializeField] private Canvas titleCanvas;
		[SerializeField] private Canvas resultCanvas;
		[SerializeField] private Text resultText;

		void Awake() {
			#if !UNITY_EDITOR
				Debug.unityLogger.logEnabled = false;
			#endif

			//SetResoultion
			//Screen.SetResolution(720, 1280, false);
			Screen.SetResolution(360, 640, false);
		}

		void Start () {
			if(Communicate.CommunicateManager.Instance.GameResult) {
				Communicate.CommunicateManager.Instance.GameResult = false;
				this.titleCanvas.gameObject.SetActive(false);
				this.resultCanvas.gameObject.SetActive(true);
				if(Communicate.CommunicateManager.Instance.GameWin) {
					this.resultText.text = "Win";
				} else {
					this.resultText.text = "Lose";
				}
			} else {

			}
			PhotonNetwork.isMessageQueueRunning = true;
		}

		void Update () {

		}

		public void OnStartButtonClicked() {
			SceneManager.LoadScene("Join");
		}

		public void OnQuitButtonClicked() {
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else 
			Application.Quit();
			#endif
		}

		public void OnToTitleButtonClicked() {
			this.titleCanvas.gameObject.SetActive(true);
			this.resultCanvas.gameObject.SetActive(false);
		}
	}
}