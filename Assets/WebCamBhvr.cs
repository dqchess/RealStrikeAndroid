using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Camera))]
public class WebCamBhvr : MonoBehaviour {
    private string deviceName;
    public Renderer mQuad;
    private Camera mCamera;
    public bool didUpdateThisFrame{
        get{
            if(tex != null)
                return tex.didUpdateThisFrame;
            return false;
        }
    }
    private WebCamTexture tex;
    #if UNITY_IOS
    private Vector2 resSize = new Vector2(1280, 720);
    #else
    private Vector2 resSize = new Vector2(720, 576);
    #endif
    public Vector2 Size {
        get {
            if (tex != null)
                return new Vector2(tex.width, tex.height);
            else return Vector2.zero;
        }
    }
    // Use this for initialization
    void Start()
    {
        if (mQuad == null)
            mQuad = GetComponentInChildren<Renderer>();
        if(mCamera == null)
            mCamera = GetComponent<Camera>();
        InitQuad();
        StartCoroutine(InitCamera());
    }
    private void InitQuad() {
        var angleA = mCamera.fieldOfView / 2;
        var tanA = Mathf.Tan(angleA * Mathf.Deg2Rad);
        var scale = tanA * mQuad.transform.localPosition.z * 2;
        mQuad.transform.localScale = Vector3.one * scale;
    }
    protected IEnumerator InitCamera()
    {
        //获取授权  
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length > 0)
            {
                deviceName = devices[0].name;
                #if UNITY_IOS
				tex = new WebCamTexture(deviceName, (int)resSize.x, (int)resSize.y, Application.targetFrameRate);
                #else
                tex = new WebCamTexture(deviceName, (int)resSize.x, (int)resSize.y, 24);
                #endif
				mQuad.material.mainTexture = tex;
                tex.Play();
				yield return 0;
				yield return 0;
				yield return 0;
				yield return 0;
				yield return 0;
                var screenRatio = (double)Screen.width / (double)Screen.height;
                var webcamRatio = (double)tex.width / (double)tex.height;
                var ratio = screenRatio / webcamRatio;
                var scale = mQuad.transform.localScale;
                scale.x = scale.x * (float)webcamRatio;
				if(ratio > 1)
                	scale = scale * (float)ratio;
                
                scale.y = scale.y * (tex.videoVerticallyMirrored ? -1:1);
                mQuad.transform.localScale = scale;
                mQuad.enabled = true;
                Debug.Log(string.Format( "{0},{1}", tex.width,tex.height));
				mQuad.sharedMaterial.color = Color.white;
            }
        }
    }

    void OnDestroy()
    {
        mQuad.material.mainTexture = null;
        if (tex != null)
        {
            tex.Stop();
            Destroy(tex);
            tex = null;
        }
    }

    public Color GetPixel(int x,int y) {
        if (tex != null)
            return tex.GetPixel(x, y);
        return Color.black;
    }
}
