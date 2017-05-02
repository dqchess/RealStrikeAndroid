using UnityEngine;
using System.Collections;

public class ArmoryCamBhvr : MonoBehaviour {
    public float FOV = 30f;
	// Use this for initialization
	void Start () {
        var ratio = (float)Screen.height / Screen.width;
        var cam = GetComponent<Camera>();
        cam.fieldOfView = FOV * ratio;
        GetComponent<TBPinchZoom>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
