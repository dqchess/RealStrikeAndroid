using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gravity : MonoBehaviour {

	private float speedY = 3;
	float b = 0;
	public float a = 10.0f;
	private float speedX = 50;
	private float speedZ = 50;
	private GameObject shadow;
	private bool isDestroy = false;

	void Start()
	{
		//shadow = this.gameObject.transform.FindChild ("Shadow").gameObject;
		this.gameObject.transform.localEulerAngles = new Vector3 (0,180,0);
		speedY = Random.Range(6,9);
		speedX = Random.Range (30,60);
		speedZ = Random.Range (30,60);
		b = speedY;
		if(this.transform.localPosition.z > 0){
			speedZ = speedZ * (-1.0f);
		}

		if (this.transform.localPosition.x > 0) {
			speedX = speedX * (-1.0f);
		}

		//Debug.Log (speedX +" " + speedZ);
	}
		

	void Update()
	{
		
		b -= Time.deltaTime * a;

		if (this.transform.localPosition.y >= 0.08f) {
			transform.Translate(0,b,0);
			transform.Translate (Time.deltaTime*speedX,0,Time.deltaTime*speedZ,this.transform.parent.transform);
		//	shadow.transform.localPosition = new Vector3 (0,this.transform.localPosition.y * (-1.0f),0);
		}else{
			if (isDestroy == true)
				return;
			isDestroy = true;
			this.gameObject.GetComponent<MeshRenderer> ().enabled = false;
			this.gameObject.GetComponent<SphereCollider> ().enabled = false;
		//	shadow.SetActive (false);
			Invoke ("DestroyObject",2);
		}
		 
	}

	private void DestroyObject(){
		Destroy (this.gameObject);
	}
}
