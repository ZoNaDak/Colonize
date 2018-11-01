using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colonize.Unit.Piece;

namespace Colonize.Unit.Bullet {
	public class Arrow : Photon.MonoBehaviour {
		private IUnit target;
		private float speed = 3.0f;
		private int damage;
		private Vector2 dir;

		void Start () {

		}

		void Update () {
		}

		void OnTriggerEnter2D(Collider2D _collider) {
			if(!this.photonView.isMine) {
				return;
			}

			if(_collider.gameObject == this.target.GetGameObject()) {
				this.target.Damaged(this.damage);
				SetDestroyOnPhoton();
				this.photonView.RPC("SetDestroyOnPhoton", PhotonTargets.Others);
			}
		}

		private void SetDirectionForTarget() {
			this.dir = (this.target.GetPos() - (Vector2)this.transform.position).normalized;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			this.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
		}

		public void Init(IUnit _target, int _damage) {
			this.target = _target;
			this.damage = _damage;
			if(this.photonView.isMine) {
				StartCoroutine(Move());
			}
		}

		private IEnumerator Move() {
			while(true) {
				if(this.target.GetDead()) {
					SetDestroyOnPhoton();
					this.photonView.RPC("SetDestroyOnPhoton", PhotonTargets.Others);
					break;
				}
				SetDirectionForTarget();
				yield return new WaitForSecondsRealtime(0.01f);
				this.transform.position += (Vector3)this.dir * this.speed;
			}
		}

		//Photon
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		}

		[PunRPC]
		private void SetDestroyOnPhoton(){
			StopAllCoroutines();
			Destroy(this.gameObject, 0.2f);
			this.gameObject.SetActive(false);
		}
	}
}