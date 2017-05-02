using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// henry huang 
/// 2016-8-10
/// gun clip bhvr
/// </summary>
public class GunMagazineBhvr : MonoBehaviour {
    public Text GunMagazine;
    public int NumberBullet;
    public int MaxNumberBullet;
    public SimpleEvent onUseUp;
    void Start() {
        ReloadComplete();
    }
    public virtual void Fire() {
        GunMagazine.text = (--NumberBullet).ToString();
        if (NumberBullet <= 0)
        {
            if (onUseUp != null)
                onUseUp.Invoke();
        }
        
    }
    public virtual void ReloadComplete() {
        NumberBullet = MaxNumberBullet;
        GunMagazine.text = NumberBullet.ToString();
    }
    public virtual void ReInit(int NumberBullet) {
        this.NumberBullet = NumberBullet;
        MaxNumberBullet = NumberBullet;
        GunMagazine.text = NumberBullet.ToString();
    }
}
