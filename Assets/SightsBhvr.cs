using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// xbhuang
/// 2016-10-5
/// </summary>
namespace Prismnova { 
    [RequireComponent(typeof(WeaponBhvr))]
    public class SightsBhvr : MonoBehaviour {
        private Image mImage;
        public Sprite sprite;
        private WeaponBhvr mWeaponBhvr;
        private Sprite lastSprite;
        private GameObject mCanvas;
        private Transform normalAim;
        public float Magnification = 2.0f;
        public bool Automatic = false;
        private bool isAim;
        private Transform mBackgroundPlaneTrans;
        private Vector3 camBgPosition = Vector3.zero;
        // Use this for initialization
        void Start () {
            mWeaponBhvr = GetComponent<WeaponBhvr>();
            if (mWeaponBhvr.CurMode == WeaponMode.Watch)
            {
                Destroy(this);
                return;
            }
            mWeaponBhvr.onAim.AddListener(OnAim);
            mWeaponBhvr.onReload.AddListener(OnReload);
            var plane = FindObjectOfType<Vuforia.BackgroundPlaneBehaviour>();
            if (plane != null)
                mBackgroundPlaneTrans = plane.transform;
            else
                mBackgroundPlaneTrans = FindObjectOfType<WebCamBhvr>().mQuad.transform;
            mCanvas = GameObject.Find("SightsCanvas");
            if (mCanvas == null)
            {
                Destroy(this);
                return;
            }
            var imageGo = mCanvas.transform.Find("Image");
            normalAim = mCanvas.transform.Find("NormalAim");
            normalAim.gameObject.SetActive(false);
            mImage = imageGo.GetComponent<Image>();
            if (sprite != null)
                mImage.sprite = sprite;
        }
        protected virtual void OnAim(bool bl) {
            isAim = bl;
            Aim(bl);
        }
        private void Aim(bool bl) {
            if (mImage == null)
                return;
            mImage.enabled = bl;
            if (bl)
            {
                if (mBackgroundPlaneTrans != null)
                {
                    camBgPosition = mBackgroundPlaneTrans.transform.localPosition;
                    mBackgroundPlaneTrans.transform.localPosition /= Magnification;
                }
                renderers = mWeaponBhvr.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (var item in renderers)
                    item.enabled = false;
            }
            else
            {

                if (mBackgroundPlaneTrans != null && camBgPosition != Vector3.zero)
                    mBackgroundPlaneTrans.transform.localPosition = camBgPosition;
                if(renderers != null)
                { 
                    foreach (var item in renderers)
                        item.enabled = true;
                }

            }
            normalAim.gameObject.SetActive(false);
        }

        protected virtual void OnLoadBullets() {
            if (isAim && !Automatic) 
                Aim(false);
        }
        protected virtual void OnLoadBulletsComplete() {
            if (isAim && !Automatic)
                Aim(true);
        }
        protected virtual void OnReload() {
            if (isAim)
                Aim(false);
        }
        protected virtual void OnReloadComplete() {
            if (isAim)
                Aim(true);
        }
        void OnDisable()
        {
            if (normalAim == null)
                return;
            OnAim(false);
            normalAim.gameObject.SetActive(true);
        }
        void OnDestroy() {
            if (mImage == null)
                return;
            mImage.enabled = false;
        }
        private SkinnedMeshRenderer[] renderers;
    }
}