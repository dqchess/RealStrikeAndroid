using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using UnityEngine.Events;
namespace Prismnova {
    public class ProductManager : MonoBehaviour {
        #region Instance
        static bool applicationIsQuitting = false;
        private static ProductManager _instance;
        public static ProductManager Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    //Debug.Log(typeof(T) + " [Mog.Singleton] is already destroyed. Returning null. Please check HasInstance first before accessing instance in destructor.");
                    return null;
                }
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ProductManager>();
                    if (_instance == null)
                    {
                        var prefab = Resources.Load<GameObject>("WeaponProductManager");
                        _instance = Instantiate(prefab).GetComponent<ProductManager>();
                        _instance.gameObject.name = _instance.GetType().Name;
                        DontDestroyOnLoad(_instance);
                    }
                    _instance.UnlockPlayerIAPProducts();
                }
                return _instance;
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
            applicationIsQuitting = true;
            //Debug.Log(typeof(T) + " [Mog.Singleton] instance destroyed with the OnDestroy event");
        }

        protected virtual void OnApplicationQuit()
        {
            _instance = null;
            applicationIsQuitting = true;
            //Debug.Log(typeof(T) + " [Mog.Singleton] instance destroyed with the OnApplicationQuit event");
        }
        #endregion
        [SerializeField]
        private WeaponProducts[] DefaultProducts;
        [SerializeField]
        private IAPProductPackage[] IAPProductPackages;
        private Dictionary<string, WeaponProduct> mHoldWeaponProductDic = null;
        private Dictionary<string, WeaponProduct> HoldWeaponProductDic {
            get {
                if (mHoldWeaponProductDic == null)
                {
                    mHoldWeaponProductDic = new Dictionary<string, WeaponProduct>();
                    foreach (var item in DefaultProducts)
                    {
                        foreach (var i in item.products)
                            mHoldWeaponProductDic[i.name] = i;
                    }
                }
                return mHoldWeaponProductDic;
            }
        }

        public WeaponProduct GetProduct(string productName) {
            WeaponProduct product;
            HoldWeaponProductDic.TryGetValue(productName, out product);
            return product;
        }
        /// <summary>
        /// 解锁产品
        /// </summary>
        /// <param name="productName"></param>
        private void UnlockProduct(string productName, bool unlock) {
            var product = GetProduct(productName);
            if (product == null)
            {
                Debug.LogError("can not find product name : " + productName);
                return;
            }
          //  product.unlock = unlock;
			product.unlock = true;
			Debug.Log ("在这里更改了解锁全部抢！！！！");
        }
        /// <summary>
        /// 根据IAPId解锁产品
        /// </summary>
        /// <param name="IAPId"></param>
        private void UnlockProductsByIAPId(string IAPId, bool unlock = true) {
            var p = GetIAPProduct(IAPId);
            if (p == null) {
                Debug.LogError("can not find iap id package: " + IAPId);
                return;
            }
            UnlockIAPProduct(p,unlock);
        }

        #region IAP 
        /// <summary>
        /// 玩家购买的IAP产品缓存的Key
        /// </summary>
        const string PLAYER_IAP_PRODUCT_IDS = "PLAYER_IAP_PRODUCT_IDS";
        /// <summary>
        /// 购买解锁IAP产品
        /// </summary>
        /// <param name="IAPId"></param>
        public void UnlockIAPProduct(string IAPId)
        {
            IAPIds.Add(IAPId);
            var json = JsonConvert.SerializeObject(IAPIds);
            PlayerPrefs.SetString(PLAYER_IAP_PRODUCT_IDS, json);
            UnlockProductsByIAPId(IAPId);
        }
        /// <summary>
        /// 根据ID获取IAP产品
        /// </summary>
        /// <param name="IAPId"></param>
        /// <returns></returns>
        public IAPProductPackage GetIAPProduct(string IAPId)
        {
            foreach (var item in IAPProductPackages)
            {
                if (IAPId.EndsWith(item.id))
                    return item;
            }
            return null;
        }
        public IAPProductPackage[] GetIAPProducts() {
            return IAPProductPackages;
        }
        /// <summary>
        /// 解锁IAP产品
        /// </summary>
        /// <param name="package"></param>
        private void UnlockIAPProduct(IAPProductPackage package,bool unlock) {
            foreach (var name in package.Names)
                UnlockProduct(name, unlock);
        }
        /// <summary>
        /// 限制访问，请访问 IAPIDs
        /// </summary>
        List<string> _iapIds;
        List<string> IAPIds {
            get {
                if(_iapIds == null) {
                    var json = PlayerPrefs.GetString(PLAYER_IAP_PRODUCT_IDS);
                    _iapIds = JsonConvert.DeserializeObject<List<string>>(json);
                }
                if (_iapIds == null)
                    _iapIds = new List<string>();
                return _iapIds;
            }
        }
        /// <summary>
        /// 是否购买过次IAP包
        /// </summary>
        /// <param name="IAPId"></param>
        /// <returns></returns>
        public bool IsHaving(string IAPId) {
            return IAPIds.Contains(IAPId);
        }
        /// <summary>
        /// 解锁玩家所购买的IAP产品
        /// </summary>
        private void UnlockPlayerIAPProducts() {
            foreach (var p in IAPProductPackages)
                UnlockProductsByIAPId(p.id, IAPIds.Contains(p.id));
        }
        #endregion
    }
    [Serializable]
    public class WeaponProduct
    {
        public string name;
        public Type type;
        public bool unlock
        {
            get { return _unlock; }
            set { _unlock = value; }
        }
		private bool _unlock = true;
        public string Path {
            get {
                return type.ToString() + "/" + name;
            }
        }
        //public GunProduct(string name, Type type) {
        //    this.name = name;
        //    this.type = type;
        //}
        public enum Type
        {
            Melee,
            Pistol,
            ShotGun,
            SMG,
            Rifle,
            Sniper,
            LMG
        }
    }
    [Serializable]
    public class WeaponProducts {
        public string type;
        public WeaponProduct[] products;
    }
    [Serializable]
    public class WeaponProductEvent : UnityEvent<WeaponProduct> { }
}