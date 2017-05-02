using UnityEngine;
using System.Collections;
using I2.Loc;
/// <summary>
/// henry huang 
/// 2016-8-11
/// </summary>
public class GameMain : MonoBehaviour {
    public SimpleEvent OnInited;
    void Awake() {
        DontDestroyOnLoad(this);
    }

    IEnumerator Init() {
        yield return 0;
        if (OnInited != null)
            OnInited.Invoke();
    }
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Init());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
