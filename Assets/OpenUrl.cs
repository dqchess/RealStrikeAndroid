using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class OpenUrl : MonoBehaviour {
    public string DefaultUrl;
    public OpenUrlData[] Urls;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Open() {
        if (Urls != null && Urls.Length > 0)
        {
            var list = new List<OpenUrlData>(Urls);
            var urlData = list.Find((o) => { return o.Platform == Application.platform; });
            if (urlData != null)
            {
                Application.OpenURL(urlData.Url);
            }
        }
        else {
            Application.OpenURL(DefaultUrl);
        }
    }
}
[Serializable]
public class OpenUrlData{
    public RuntimePlatform Platform;
    public string Url;
}
