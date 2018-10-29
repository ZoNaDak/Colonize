using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Colonize.ControllUI.UnitControll {
	public class InnerImage : MonoBehaviour {
        [SerializeField] private Image imageMask;
        [SerializeField] private Text goldText;

        public Image ImageMask { get { return imageMask; } }

        public void SetGold(int _gold) {
            if(goldText == null) {
                return;
            }
            goldText.text = string.Format("- {0}G", _gold);
        }
	}
}

