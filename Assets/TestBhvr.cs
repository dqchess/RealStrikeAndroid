using UnityEngine;
using System.Collections;

public class TestBhvr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    bool bl;
    public void Aim() {
        var bhvr = FindObjectOfType<WeaponBhvr>();
        if (bhvr == null)
            return;
        bl = !bl;
        bhvr.Aim(bl);
        
    }

    public void Switch() {
        var bhvr = FindObjectOfType<WeaponBhvr>();
        if (bhvr == null)
            return;
        bhvr.NextWatchPoint();
    }
}
