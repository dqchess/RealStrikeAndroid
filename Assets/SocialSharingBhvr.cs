using UnityEngine;
using System.Collections;
/// <summary>
/// henry huang
/// 2016-8-10
/// social sharing call back
/// </summary>
public class SocialSharingBhvr : MonoBehaviour {
    public BoolEvent onSocialSharingPostResult;
    // Use this for initialization
    void Start () {
        AddEventlistener();
    }
    void AddEventlistener() {
        IOSSocialManager.OnTwitterPostResult += OnSocialSharingPostResult;
        IOSSocialManager.OnFacebookPostResult += OnSocialSharingPostResult;
        IOSSocialManager.OnInstagramPostResult += OnSocialSharingPostResult;
        IOSSocialManager.OnMailResult += OnSocialSharingPostResult;
    }
    protected virtual void OnSocialSharingPostResult(SA.Common.Models.Result res) {
        if (onSocialSharingPostResult != null)
            onSocialSharingPostResult.Invoke(res.IsSucceeded);
    }

	public void SaveScreenshotToCameraRoll (){
		//IOSCamera.OnImageSaved += OnImageSaved;
		//IOSCamera.Instance.SaveScreenshotToCameraRoll ();
		Application.CaptureScreenshot("RealStrike.png",0);
	}
	public void PostScreenshot(){
		StartCoroutine (PostScreenshotCoroutine());
	}

	private IEnumerator PostScreenshotCoroutine() {

		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();
//		IOSCamera.OnImageSaved += OnImageSaved;
//		IOSCamera.Instance.SaveTextureToCameraRoll (tex);
		IOSSocialManager.Instance.ShareMedia("Real Strike.", tex);
		Destroy(tex);
	}

	private void OnImageSaved (SA.Common.Models.Result result) {
		IOSCamera.OnImageSaved -= OnImageSaved;
		if(result.IsSucceeded) {
			IOSMessage.Create("Success", "Image Successfully saved to Camera Roll");
		} else {
			IOSMessage.Create("ERROR", "Image Save Failed");
		}
	}
}
