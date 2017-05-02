using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class WeaponBhvr : MonoBehaviour, IEventSystemHandler
{
    public WeaponMode CurMode = WeaponMode.Battle;
    public int NumberBullet = 30;
    private Animator animator;
    public Transform NormalCameraPoint;
    public Transform AimCameraPoint;
    public Transform DefaultWatchPoint;
    public SkinnedMeshRenderer WeaponMesh;
    public SkinnedMeshRenderer ArmMesh;
    public Transform GunFlashAnchor;
    public GunFlash GunFlashGo;
    public AudioClip[] AudioClips;
    public Ease TweenEase = Ease.OutCirc;
    public float TweenDuration = 2f;



    public SimpleEvent onFire;
    public SimpleEvent onReloadComplete;
    public SimpleEvent onReload;


    private GameObject[] mWatchPoints;
    private SkinnedMeshRenderer[] mRenderers;
    bool isStarted;
    // Use this for initialization
    void Start () {
        Resources.UnloadUnusedAssets();
        mRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var renderer in mRenderers)
            renderer.updateWhenOffscreen = true;
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        mWatchPoints = GameObject.FindGameObjectsWithTag("WatchPoint");
        if (DefaultWatchPoint.transform == mWatchPoints[0].transform)
            curWatchIndex++;
        switch (CurMode)
        {
            case WeaponMode.Watch:
                if (WeaponMesh != null) 
                    transform.position = -WeaponMesh.bounds.center;
                SwitchCam(DefaultWatchPoint);
                ArmMesh.enabled = false;
                break;
            default:
                SwitchCam(NormalCameraPoint);
                break;
        }
        isStarted = true;

    }
    int curWatchIndex = 0;
    public void NextWatchPoint() {
        if (mWatchPoints == null || mWatchPoints.Length == 0)
            return;
        if (mWatchPoints.Length <= curWatchIndex)
            curWatchIndex = 0;
        var target = mWatchPoints[curWatchIndex].transform;
        SwitchCam(target);
        curWatchIndex++;
    }
    void SwitchCam(Transform target) {
        
        var trans = Camera.main.transform;
        if (trans.parent != null)
            trans = trans.parent;
        if (target == null)
            target = transform.Find("Camera001");
        var lookAtPoint = transform.Find(target.name+ ".Target");
        if (lookAtPoint == null) {
            Debug.LogError("can not find look at point :" + target.name + ".Target");
            return;
        }
        trans.DOMove(target.position, TweenDuration).SetEase(TweenEase).SetUpdate(UpdateType.Fixed);
        var direction = (lookAtPoint.position - target.position);
        var rotate = Quaternion.LookRotation(direction);
        trans.DORotate(rotate.eulerAngles, TweenDuration).SetEase(TweenEase).SetUpdate(UpdateType.Fixed);
    }
    
    public void PlayAudio(string clipname) {
        var aSource = GetComponent<AudioSource>();
        AudioClip clip = null;
        foreach (var item in AudioClips)
        {
            if (item != null && item.name.Contains(clipname))
                clip = item;
        }
        if (clip != null)
        {
            //AudioSource.PlayClipAtPoint(clip,transform.position);
            aSource.Stop();
            aSource.PlayOneShot(clip);
        }
        else
            Debug.LogWarning("can not find clipname : " + clipname);
    }
    public BoolEvent onAim;
    public virtual void Aim(bool bl) {
        if (!isStarted || !gameObject.activeSelf)
            return;
        if (onAim != null)
            onAim.Invoke(bl);
        if (bl)
            SwitchCam(AimCameraPoint);
        else
            SwitchCam(NormalCameraPoint);
    }
    public virtual void Fire() {
        animator.SetBool("fire",true);
    }
    public virtual void Stop() {
        animator.SetBool("fire", false);
    }
    public virtual void Reload() {
        animator.SetTrigger("reload");
		if (onReload != null)
            onReload.Invoke();
    }

    public void OnFire() {
        if (onFire != null)
            onFire.Invoke();
        PlayGunFlash();
    }
    public void PlayGunFlash() {
        if (GunFlashAnchor != null)
        {
            if(GunFlashGo == null)
                GunFlashGo = Resources.Load<GunFlash>("Prefab/Widget/GunFlash01");
            var go = Instantiate(GunFlashGo);
            go.transform.SetParent(GunFlashAnchor.transform, false);
            go.transform.localPosition = Vector3.zero;
            //go.transform.position = GunFlashAnchor.transform.position;
            go.gameObject.SetActive(true);
        }
    }
    public void OnReloadComplete() {
        Debug.Log("onreload complete!");
        animator.ResetTrigger("reload");
		if (onReloadComplete != null)
            onReloadComplete.Invoke();
    }

    [Serializable]
    public class AudioPair{
        public string Key;
        public AudioClip Val;
    }
}

public enum WeaponMode {
    None,
    Battle,
    Watch
}
