using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aimManager : MonoBehaviour {

	public GameObject aimPerfab;
	public GameObject goldPerfab;
	public GameObject addScoreEnemyPerfab;
	public GameObject reduceScoreEnemyPerfab;
	public GameObject addTimeEnemyPerfab;
	public GameObject reduceTimeEnemyPerfab;
	public GameObject explodeGoldEnemyPerfab;
	public GameObject bombEnemyPerfab;

	public SimpleEvent reStartScore;
	public SimpleEvent calculateScore;
	public bool isInMark = false;
	private float nowTime;
	private float beforeTime;
	private float goldTime;
	//计时器面板
	public GameObject TimePlane;
	private Text TimeText;
	private float gameTime;
	private float[] reduceScoreRandom = new float[7];
	private GameObject addTimeFiveText;
	private GameObject reduceTimeFiveText;

	//控制时间继续，不出怪的变量
	private bool stopCreat = false;

	//倒计时
	private string[] timeDown = {"3","2","1","GO",""};
	private float index = 0;
	public GameObject timeDownPlane;
	private Text timeDownPlaneText;
	private bool isFirstTime = true;


	// Use this for initialization
	void Start () {
		TimeText = TimePlane.GetComponent<Text> ();
		timeDownPlaneText = timeDownPlane.GetComponent<Text> ();
		addTimeFiveText = TimePlane.transform.Find ("+5Time").gameObject;
		reduceTimeFiveText = TimePlane.transform.Find ("-5Time").gameObject;
		nowTime = Time.time;
		beforeTime = nowTime;
		goldTime = nowTime;
		gameTime = 0;
		//减分随机时间
		//根据时间进程的不同分情况出目标
		//16-23s 随机一个
		//24-43s 随机两个
		//44-53s 随机一个
		//56-65s 随机一个
		//66-80s 随机两个
		reduceScoreRandom[0] = (float)Random.Range (16,24);
		reduceScoreRandom[1] = (float)Random.Range (24,44);
		reduceScoreRandom[2] = (float)Random.Range (24,44);
		reduceScoreRandom[3] = (float)Random.Range (44,54);
		reduceScoreRandom[4] = (float)Random.Range (56,66);
		reduceScoreRandom[5] = (float)Random.Range (66,80);
		reduceScoreRandom[6] = (float)Random.Range (66,80);
	}

	void FixedUpdate(){
		//如果mark扫描成功
		if (isInMark) {
			
			if (index >= 4) {
				nowTime = nowTime + Time.deltaTime;
				gameTime = gameTime + Time.deltaTime;
				//当游戏时间超过100s后不再创建任何东西
				if ((int)gameTime > 80) {
					if (calculateScore != null)
						calculateScore.Invoke ();
					return;
				}
				//TimeText.text = ((int)(80 - gameTime)).ToString () + "s";
				TimeText.text = (80 - (int)gameTime).ToString ();
				//每隔5秒创建两个金币
				if (nowTime - goldTime > 15.0f) {
					CreatGold ();
					CreatGold ();
					goldTime = nowTime;
				}
				//开始进行创建模型
				CreatSimpleEnemy ();
				CreatAddScoreEnemy ();
				CreatReduceScoreEnemy ();
				CreatAddTimeEnemy ();
				CreatReduceTimeEnemy ();
				CreatExplodeGold ();
				CreatBomb ();
			} else {
				if (isFirstTime) {
					transform.GetComponent<AudioSource> ().Play ();
					isFirstTime = false;
				}
				index = index + Time.deltaTime;
				timeDownPlaneText.text = timeDown[(int)index];
			}

		}
	}

	/// <summary>
	/// 停止创造敌人5s，但时间继续
	/// </summary>
	public void StopCreatEnemy(){
		stopCreat = true;
		for (int i = 0; i < transform.childCount; i++) {  
			Destroy (transform.GetChild (i).gameObject);  
		}  
		Invoke ("Recover",5.0f);
	}

	private void Recover(){
		stopCreat = false;
	}

	/// <summary>
	/// 攻击到了增加时间的enemy,时间向后退5s
	/// </summary>
	public void AddTime(){
		gameTime = gameTime - 5.0f;
		if (gameTime < 0)
			gameTime = 0;
		TimeText.text = ((int)gameTime).ToString() + "s";
		addTimeFiveText.SetActive (true);
	}
	/// <summary>
	/// 攻击到了减少时间的enemy,时间向前走5s
	/// </summary>
	public void ReduceTime(){
		gameTime = gameTime + 5.0f;
		if (gameTime >= 80)
			gameTime = 80.0f;
		TimeText.text = ((int)gameTime).ToString() + "s";
		reduceTimeFiveText.SetActive (true);
	}
	/// <summary>
	/// 攻击到了爆炸金币奖励，创建10个金币
	/// </summary>
	public void ExplodeGold(){
		for (int i = 0; i < 10; i++) {
			CreatGold ();
		}
	}
	/// <summary>
	/// Creats the bomb.
	/// </summary>
	private void CreatBomb(){
		if (gameTime <= 20.02f && gameTime >= 20.0f) {
			StartCreat (bombEnemyPerfab);
		} else if (gameTime <= 40.02f && gameTime >= 40.0f) {
			StartCreat (bombEnemyPerfab);
		}else if (gameTime <= 60.02f && gameTime >= 60.0f) {
			StartCreat (bombEnemyPerfab);
		}
	}

	/// <summary>
	/// Creats the explode gold.
	/// </summary>
	private void CreatExplodeGold(){
//		if (gameTime <= 35.02f && gameTime >= 35.0f) {
//			StartCreat (explodeGoldEnemyPerfab);
//		} else if (gameTime <= 56.02f && gameTime >= 56.0f) {
//			StartCreat (explodeGoldEnemyPerfab);
//		}else if (gameTime <= 73.02f && gameTime >= 73.0f) {
//			StartCreat (explodeGoldEnemyPerfab);
//		}

	}


	/// <summary>
	/// 19s,28s,38s,48s,65s,创建减时间enemy
	/// </summary>
	private void CreatReduceTimeEnemy(){
		if (gameTime <= 19.02f && gameTime >= 19.0f) {
			StartCreat (reduceTimeEnemyPerfab);
		} else if (gameTime <= 28.02f && gameTime >= 28.0f) {
			StartCreat (reduceTimeEnemyPerfab);
		} else if (gameTime <= 38.02f && gameTime >= 38.0f) {
			StartCreat (reduceTimeEnemyPerfab);
		} else if (gameTime <= 48.02f && gameTime >= 48.0f) {
			StartCreat (reduceTimeEnemyPerfab);
		} else if (gameTime <= 65.02f && gameTime >= 65.0f) {
			StartCreat (reduceTimeEnemyPerfab);
		}
	}

	/// <summary>
	/// 根据时间进程的不同分情况出目标
	/// 32s,45s,42s,67s,63s
	/// 创建增加时间enemy
	/// </summary>
	private void CreatAddTimeEnemy(){
		if (gameTime <= 32.02f && gameTime >= 32.0f) {
			StartCreat (addTimeEnemyPerfab);
		} else if (gameTime <= 42.02f && gameTime >= 42.0f) {
			StartCreat (addTimeEnemyPerfab);
		} else if (gameTime <= 45.02f && gameTime >= 45.0f) {
			StartCreat (addTimeEnemyPerfab);
		} else if (gameTime <= 63.02f && gameTime >= 63.0f) {
			StartCreat (addTimeEnemyPerfab);
		} else if (gameTime <= 67.02f && gameTime >= 67.0f) {
			StartCreat (addTimeEnemyPerfab);
		}
	}

	/// <summary>
	/// 创建减分飞碟
	/// </summary>
	private void CreatReduceScoreEnemy(){
		//根据时间进程的不同分情况出目标
		//16-23s 随机一个
		//24-43s 随机两个
		//44-53s 随机一个
		//56-65s 随机一个
		//66-80s 随机两个
		if (gameTime <= (reduceScoreRandom[0]+0.02f) && gameTime >=(reduceScoreRandom[0])) {
			StartCreat (reduceScoreEnemyPerfab);
		} else if (gameTime <= (reduceScoreRandom[1]+0.02f) && gameTime >= (reduceScoreRandom[1])) {
			StartCreat (reduceScoreEnemyPerfab);
		}else if (gameTime <= (reduceScoreRandom[2]+0.02f) && gameTime >= (reduceScoreRandom[2])){
			StartCreat (reduceScoreEnemyPerfab);
		}else if (gameTime <= (reduceScoreRandom[3]+0.02f) && gameTime >= (reduceScoreRandom[3])) {
			StartCreat (reduceScoreEnemyPerfab);
		}else if (gameTime<= (reduceScoreRandom[4]+0.02f) && gameTime >= (reduceScoreRandom[4])) {	
			StartCreat (reduceScoreEnemyPerfab);
		}else if (gameTime<= (reduceScoreRandom[5]+0.02f) && gameTime >= (reduceScoreRandom[5])) {
			StartCreat (reduceScoreEnemyPerfab);
		}else if (gameTime<= (reduceScoreRandom[6]+0.02f) && gameTime >= (reduceScoreRandom[6])) {
			StartCreat (reduceScoreEnemyPerfab);
		}
	}

	/// <summary>
	/// 创建加分飞碟
	/// </summary>
	private void CreatAddScoreEnemy(){
		//根据时间进程的不同分情况出目标
		//20s,25s，35s，50s，60s，68s,75s都出一个
		if (gameTime <= 20.02f && gameTime >20.0F) {
			StartCreat (addScoreEnemyPerfab);
		}else if (gameTime <= 25.02f && gameTime > 25.0F) {
			StartCreat (addScoreEnemyPerfab);
		}else if (gameTime <= 35.02f && gameTime > 35.0F) {
			StartCreat (addScoreEnemyPerfab);
		}else if (gameTime <= 50.02f && gameTime > 50.0F) {
			StartCreat (addScoreEnemyPerfab);
		}else if (gameTime <= 60.02f && gameTime > 60.0F) {
			StartCreat (addScoreEnemyPerfab);
		}else if (gameTime<= 68.02f && gameTime > 68.0F) {
			StartCreat (addScoreEnemyPerfab);
		}else if (gameTime <= 75.02f && gameTime > 75.0F) {
			StartCreat (addScoreEnemyPerfab);
		}
	}


	/// <summary>
	/// 创建普通的飞碟
	/// </summary>
	private void CreatSimpleEnemy(){
		//根据时间进程的不同分情况出目标
		//1——15s 每2s一个目标
		//16-25s 每1.5s一个目标
		//26-74s 每1s一个目标
		//75-89s 每1.5s一个目标
		//90-100s 每0.5s一个目标
		//时间间隔2秒创建一个飞镖
		if (gameTime <= 15.0f) {
			Creat (2.0f);
			return;
		} else if (gameTime <= 23.0f) {
			Creat (1.5f);
			return;
		} else if (gameTime <= 43.0f) {
			Creat (1.0f);
			return;
		} else if (gameTime <= 53.0f) {
			Creat (0.5f);
			return;
		} else if (gameTime <= 55.0f) {
			return;
		} else if (gameTime <= 65.0f) {
			Creat (1.5f);
			return;
		} else if (gameTime <= 80.0f) {
			Creat (0.3f);
			return;
		} else {
			return;
		}
	}

	/// <summary>
	/// 重新开始
	/// </summary>
	public void ReStart(){
		if (gameTime >= 80) {
			gameTime = 0;
			index = 0;
			isFirstTime = true;
			if (reStartScore != null)
				reStartScore.Invoke ();
		}
	}

	private void Creat(float deltaTime){
		if (nowTime - beforeTime > deltaTime) {
			StartCreat (aimPerfab);
			beforeTime = nowTime;
		}
	}
	/// <summary>
	/// 判断是否扫描到mark
	/// </summary>
	public void InMark(){
		isInMark = true;
	}
	/// <summary>
	/// 判断是否脱离mark
	/// </summary>
	public void OutMark(){
		isInMark = false;
	}

	//创建新飞盘
	private void StartCreat(GameObject perfab){

		if (stopCreat)
			return;

		GameObject prefab = GameObject.Instantiate (perfab,Vector3.zero,Quaternion.identity);

		prefab.transform.SetParent (this.gameObject.transform);
		//随机坐标
		float X = Random.Range (0,50) / 100.0F;
		if (Random.Range (0, 2) == 1) {
			X = X * (-1.0f);
		}

		float Z = Random.Range (0,30) / 100.0F;
		if (Random.Range (0, 2) == 1) {
			Z = Z * (-1.0f);
		}

		prefab.gameObject.transform.localPosition = new Vector3 (X,0.08f,Z);
		prefab.gameObject.transform.localScale = new Vector3 (2F,2F,2F);

		perfab.SetActive (true);

	}

	//创建金币
	private void CreatGold(){
		if (stopCreat)
			return;
		GameObject gold = GameObject.Instantiate (goldPerfab,Vector3.zero,Quaternion.identity);

		gold.transform.SetParent (this.gameObject.transform);

		float X = Random.Range (0,50) / 100.0F;
		if (Random.Range (0, 2) == 1) {
			X = X * (-1.0f);
		}

		float Z = Random.Range (0,30) / 100.0F;
		if (Random.Range (0, 2) == 1) {
			Z = Z * (-1.0f);
		}

		float Y = Random.Range (4,30) / 100.0F;

		gold.transform.localPosition = new Vector3 (X,Y,Z);

		gold.transform.localScale = new Vector3 (2.0f,2.0f,2.0f);

		gold.transform.localEulerAngles = new Vector3 (90.0f,0,0);

		gold.SetActive (true);

		//gold.transform.FindChild ("Shadow").gameObject.transform.localPosition = new Vector3 (0,0,gold.transform.localPosition.y * (10.0f * 1.23f));
	}
}
