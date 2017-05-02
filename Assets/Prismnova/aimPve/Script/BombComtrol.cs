using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombComtrol : MonoBehaviour {

	private GameObject shadow;
	private int bloodValue = 6;
	private aimManager aimM;
	private Score score;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3 (2.4f,2.4f,2.4f);
		aimM = this.transform.parent.GetComponent<aimManager> ();
		score = GameObject.Find ("Canvas/Score").GetComponent<Score>();
	//	shadow = this.transform.FindChild("shadow").gameObject;
		this.gameObject.transform.localEulerAngles = new Vector3 (0,0,0);
		this.transform.DOLocalMoveY (0.4f, 5.0f, false);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.localPosition.y >= 0.398f) {
			aimM.StopCreatEnemy ();
			Destroy (this.gameObject);
		} 
//		shadow.transform.localPosition = new Vector3 (this.transform.localPosition.x,this.transform.localPosition.y * (-0.4f),this.transform.localPosition.z);
	}

	public void reduceBlood(){
		bloodValue--;
		if (bloodValue == 0) {
			score.ShootAddTenScoreEnemy ();
			Bomb ();
		}
	}

	public void Bomb(){
		this.gameObject.GetComponent<SphereCollider> ().enabled = false;
		this.transform.FindChild ("group1/two___wings2_Object02/polySurface1").GetComponent<MeshRenderer> ().enabled = false;
		this.transform.FindChild ("group1/two___wings2_Object02/polySurface2").GetComponent<MeshRenderer> ().enabled = false;
		this.transform.FindChild ("Object02").GetComponent<MeshRenderer> ().enabled = false;
		this.transform.FindChild ("Sphere").GetComponent<MeshRenderer> ().enabled = false;
		this.transform.FindChild("Blood").gameObject.SetActive(true);
		this.transform.GetComponent<AudioSource> ().Play ();
		Invoke ("destroy",1.5f);
	}

	private void destroy(){
		Destroy (this.gameObject);
	}
}
