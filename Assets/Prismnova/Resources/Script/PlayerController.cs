using UnityEngine;
using System.Collections;
using System;

namespace Prismnova
{
	/// <summary>
	/// henry huang
	/// 2016-8-10
	/// sample player controller
	/// </summary>
	public class PlayerController : MonoBehaviour
	{
		public SimpleEvent onFire;
		public SimpleEvent onReloadComplete;
		//public BoolEvent onAim;
		public BoolEvent unAim;
		private WeaponBhvr mWeapon;
		public PlayerState State;
		public Transform Anchor;
		public string DefaultWeaponName = "MK46";
		public GameObject score;
		private Score scorePlane;
		// Use this for initialization
		void Start()
		{
			scorePlane = score.GetComponent<Score> ();
			SwitchWeapon(DefaultWeaponName);
		}
		public virtual void SwitchWeapon(string WeaponName)
		{
			var product = ProductManager.Instance.GetProduct(DefaultWeaponName);
			SwitchWeapon(product);
		}
		public virtual void SwitchWeapon(WeaponProduct product)
		{
			if (mWeapon != null)
			{
				mWeapon.gameObject.SetActive(false);
				UnAim();
				Destroy(mWeapon.gameObject);
			}
			var prefab = Resources.Load<GameObject>(product.Path);
			if (prefab == null)
			{
				Debug.LogError("找不到武器：" + product.Path);
				return;
			}
			var go = Instantiate(prefab);
			go.gameObject.SetActive(true);
			mWeapon = go.GetComponent<WeaponBhvr>();
			mWeapon.onFire.AddListener(OnFireHandler);
			mWeapon.onReloadComplete.AddListener(OnReloadCompleteHandler);
			State.Reset();
			var gunMagazine = GetComponent<GunMagazineBhvr>();
			gunMagazine.ReInit(mWeapon.NumberBullet);
		}


		#region Player Behaviour
		public virtual void OpenFire()
		{
			if (mWeapon == null)
				return;
			if (State.Contains(PlayerState.Reload))
				return;
			mWeapon.Fire();
		}

		public virtual void StopFiring()
		{
			if (mWeapon == null)
				return;
			mWeapon.Stop();
		}

		public virtual void SwapAim()
		{
			bool bl = State.Contains(PlayerState.Aim);
			if (!bl)
				Aim();
			else
				UnAim();

		}
		protected virtual void ApplyAim() {
			bool bl = State.Contains(PlayerState.Aim);
			if (bl)
				Aim();
			else
				UnAim();
		}

		public virtual void Aim()
		{
			State += PlayerState.Aim;
			if (unAim != null)
				unAim.Invoke(false);
			mWeapon.Aim(true);
		}

		public virtual void UnAim()
		{
			State -= PlayerState.Aim;
			if (unAim != null)
				unAim.Invoke(true);
			mWeapon.Aim(false);
		}

		public virtual void Reload()
		{
			State += PlayerState.Reload;
			mWeapon.Reload();
		}

		#endregion
		protected virtual void OnReloadCompleteHandler()
		{
			State -= PlayerState.Reload;
			if (onReloadComplete != null)
				onReloadComplete.Invoke();
		}

		private void OnFireHandler()
		{
			if (onFire != null) onFire.Invoke();
			if (State.Contains(PlayerState.Reload))
				mWeapon.Reload();
			//碰撞检测
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				//攻击到普通怪物
				if (hitInfo.collider.gameObject.CompareTag ("AimTarget")) {
					scorePlane.ShootEnemy ();
					//Destroy (hitInfo.collider.gameObject);
					congFired(hitInfo.collider.gameObject);
					return;
				}

				if (hitInfo.collider.gameObject.CompareTag ("Gold")) {
					scorePlane.ShootGold ();
					Destroy (hitInfo.collider.gameObject);
					return;
				}

				if (hitInfo.collider.gameObject.CompareTag ("addScoreTen")) {
					scorePlane.ShootAddTenScoreEnemy ();
					congFired(hitInfo.collider.gameObject);
					return;
				}

				if (hitInfo.collider.gameObject.CompareTag ("reduceScoreTen")) {
					scorePlane.ShootReduceTenScoreEnemy ();
					congFired(hitInfo.collider.gameObject);
					return;
				}

				if (hitInfo.collider.gameObject.CompareTag ("addTime")) {
					hitInfo.collider.transform.parent.GetComponent<aimManager> ().AddTime ();
					congFired(hitInfo.collider.gameObject);
					return;
				}

				if (hitInfo.collider.gameObject.CompareTag ("ReduceTime")) {
					hitInfo.collider.transform.parent.GetComponent<aimManager> ().ReduceTime ();
					congFired(hitInfo.collider.gameObject);
					return;
				}

				if (hitInfo.collider.gameObject.CompareTag ("ExplodeGold")) {
					hitInfo.collider.transform.parent.GetComponent<aimManager> ().ExplodeGold ();
					congFired(hitInfo.collider.gameObject);
					return;
				}

				if (hitInfo.collider.gameObject.CompareTag ("Bomb")) {
					hitInfo.collider.transform.GetComponent<BombComtrol> ().reduceBlood ();
					return;
				}
				//                var go = Instantiate(BulletHoles);
				//                go.transform.position = hitInfo.point;
				//                go.transform.SetParent(hitInfo.collider.transform, true);
				//                go.transform.LookAt(hitInfo.transform);
			}
		}

		private void congFired(GameObject perfab){
			//Destroy (hitInfo.collider.gameObject);
			perfab.transform.FindChild("Blood").gameObject.SetActive(true);
			perfab.transform.GetComponent<MeshRenderer> ().enabled = false;
			perfab.transform.GetComponent<SphereCollider> ().enabled = false;
			perfab.transform.GetComponent<AudioSource> ().Play ();
		}

		[Serializable]
		public struct PlayerState
		{
			public enum State
			{
				Normal = 0,
				Aim = 2,
				Reload = 4,
			}
			public State current;
			public PlayerState(State state)
			{
				this.current = state;
			}
			public static PlayerState Reload
			{
				get
				{
					return new PlayerState(State.Reload);
				}
			}
			public static PlayerState Aim
			{
				get
				{
					return new PlayerState(State.Aim);
				}
			}
			public static PlayerState Normal
			{
				get
				{
					return new PlayerState(State.Normal);
				}
			}
			public static PlayerState operator +(PlayerState lhs, PlayerState rhs)
			{
				PlayerState result = new PlayerState(lhs.current);
				result.current |= rhs.current;
				return result;
			}
			public static PlayerState operator -(PlayerState lhs, PlayerState rhs)
			{
				PlayerState result = new PlayerState(lhs.current);
				if ((result.current & rhs.current) == rhs.current)
					result.current ^= rhs.current;
				return result;
			}
			public bool Contains(PlayerState rhs)
			{
				if ((current & rhs.current) == rhs.current)
					return true;
				return false;
			}
			public void Reset()
			{
				current = State.Normal;
			}
		}
	}
}