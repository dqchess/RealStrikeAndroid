using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TakePhoto : MonoBehaviour {

	public void takePhoto(){
		//获取系统时间并命名相片名  
		System.DateTime now = System.DateTime.Now;  
		string times = now.ToString ();  
		times = times.Trim ();  
		times = times.Replace ("/","-");  
		string filename = "Screenshot"+times+".png";   
		//判断是否为Android平台  
		if (Application.platform == RuntimePlatform.Android) {  

			//截取屏幕  
			Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);  
			texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);  
			texture.Apply();  
			//转为字节数组  
			byte[] bytes = texture.EncodeToPNG();  

			string destination = "/sdcard/DCIM/ARphoto";  
			//判断目录是否存在，不存在则会创建目录  
			if (!Directory.Exists (destination)) {  
				Directory.CreateDirectory (destination);  
			}  
			string Path_save = destination+"/" + filename;  
			//存图片  
			System.IO.File.WriteAllBytes(Path_save, bytes);  
		}  
	}
}
