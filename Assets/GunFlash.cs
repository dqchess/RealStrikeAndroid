using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GunFlash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Camera.main != null)
            transform.LookAt(Camera.main.transform);
        var rz = Random.Range(0, 180);
        transform.Rotate(Vector3.forward * rz );
        Destroy(gameObject, 0.5f);
    }
}
