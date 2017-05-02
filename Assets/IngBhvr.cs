using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class IngBhvr : MonoBehaviour {
    public Text text;
    public string str;
    private string[] strs;
	// Use this for initialization
	void Start () {
        strs = str.Split(';');
        InvokeRepeating("SendLine", 0, 1);
    }
    int index = 0;
    public float lifetime = 10f;
    private int lastY;
    void SendLine() {
        if (index > strs.Length - 1)
        {
            index = 0;
            return;
        }
        var newText = Instantiate(text);
        newText.transform.SetParent(transform, false);
        newText.gameObject.SetActive(true);
        newText.text = strs[index++];
        while (true) { 
            var ry = Random.Range(-500, 500);
            if (Mathf.Abs(lastY - ry) > 30) {
                lastY = ry;
                break;
            }
        }

        newText.transform.DOLocalMoveY(lastY, 1);
        newText.transform.DOMoveX(3000, lifetime-1);
        Destroy(newText.gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
