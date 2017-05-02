using UnityEngine;
using System.Collections;
using Vuforia;
/// <summary>
/// Device light.
/// </summary>
public class DeviceLight : MonoBehaviour {
	public void Open(bool bl){
		#if UNITY_IOS
		if(bl)
		iOSTorch.On ();
		else
		iOSTorch.Off ();
		#endif

		CameraDevice.Instance.SetFlashTorchMode (bl);
	}

	void OnDestroy(){
		#if UNITY_IOS
		iOSTorch.Off ();
		#endif

		CameraDevice.Instance.SetFlashTorchMode (false);
	}
}
