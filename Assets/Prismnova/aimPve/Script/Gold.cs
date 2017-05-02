using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

	private float beforeTime;

	// Use this for initialization
	void Start () {
		beforeTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - beforeTime >= 2.0f) {
			Destroy (this.gameObject);
		}
	}
}
