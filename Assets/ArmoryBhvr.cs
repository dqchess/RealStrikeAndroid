using UnityEngine;
using System.Collections;

using DG.Tweening;

namespace Prismnova
{
    public class ArmoryBhvr : MonoBehaviour
    {
        public string DefaultWeaponName = "M4A1";
        public WeaponBhvr mWeapon;
        // Use this for initialization
        void Start()
        {
            SwitchWeapon(DefaultWeaponName);

        }
        public virtual void SwitchWeapon(WeaponProduct product)
        {
            if (mWeapon != null)
            {
                mWeapon.gameObject.SetActive(false);
                Destroy(mWeapon.gameObject);
            }
            var prefab = Resources.Load<GameObject>(product.Path);
            var go = Instantiate(prefab);
            go.transform.SetParent(transform, false);
            go.gameObject.SetActive(true);
            mWeapon = go.GetComponent<WeaponBhvr>();
            mWeapon.CurMode = WeaponMode.Watch;
        }
        public virtual void SwitchWeapon(string WeaponPath)
        {

            var product = ProductManager.Instance.GetProduct(DefaultWeaponName);
            SwitchWeapon(product);
        }
        public void NextWatchPoint()
        {
            if (mWeapon != null)
                mWeapon.NextWatchPoint();
        }
        public void OnDrag(DragGesture gesture)
        {
            switch (gesture.State)
            {
                case GestureRecognitionState.Ended:
                    transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutCubic);
                    return;
                default:
                    break;
            }
        }
    }
}