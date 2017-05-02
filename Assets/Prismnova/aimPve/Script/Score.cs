using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int scoreNumber = 0;
	public GameObject HighestText;
	private Text text;
	public GameObject lastScore;
	private GameObject addOne;
	private GameObject addFive;
	private GameObject reduceTen;
	private GameObject addThree;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("HighScore") != null)
			HighestText.GetComponent<Text> ().text = "HighestScore : " + PlayerPrefs.GetInt ("HighScore");
		text = this.transform.GetComponent<Text> ();
//		addOne = text.transform.FindChild ("+1").gameObject;
		addFive = text.transform.FindChild ("+5").gameObject;
		addThree = text.transform.FindChild ("+3").gameObject;
		reduceTen = text.transform.FindChild ("-10").gameObject;
	}

	public void CalculateHighScore(){
		//结算最高分数
		if (scoreNumber > PlayerPrefs.GetInt ("HighScore"))
			PlayerPrefs.SetInt ("HighScore",scoreNumber);
		HighestText.GetComponent<Text> ().text = "HighestScore : " + PlayerPrefs.GetInt ("HighScore");

		//游戏结束界面显示分数
		lastScore.GetComponent<Text>().text = "Score : " + scoreNumber;
	}
	/// <summary>
	/// 打到了目标
	/// </summary>
	public void ShootEnemy(){
		scoreNumber++;
		RefreshText (1);
	}
	/// <summary>
	/// 打到了金币
	/// </summary>
	public void ShootGold(){
		scoreNumber = scoreNumber + 3;
		RefreshText (3);

	}
	/// <summary>
	/// 分数清零
	/// </summary>
	public void ReStartGame(){
		scoreNumber = 0;
		RefreshText (0);
	}

	/// <summary>
	/// 打到加10分飞镖
	/// </summary>
	public void ShootAddTenScoreEnemy(){
		scoreNumber = scoreNumber + 5;
		RefreshText (5);
	}

	/// <summary>
	/// 打到减10分的飞碟
	/// </summary>
	public void ShootReduceTenScoreEnemy(){
		if (scoreNumber >= 10) {
			scoreNumber = scoreNumber - 10;
		} else {
			scoreNumber = 0;
		}

		RefreshText (-10);
	}
	//刷新ui
	private void RefreshText(int index){
		text.text =  "score："+ scoreNumber.ToString();
		switch(index){
		case 1:
			//addOne.SetActive (true);
			break;
		case 5:
			addFive.SetActive (true);
			break;
		case -10:
			reduceTen.SetActive (true);
			break;
		case 3:
			addThree.SetActive (true);
			break;


		}
	}
}
