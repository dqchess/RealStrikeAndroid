using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreShowOut : MonoBehaviour {

	public void reSetScore(){

		this.gameObject.GetComponent<DOTweenAnimation> ().DOPlay ();

	}
}
