using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightClose : MonoBehaviour {

	public Sprite closeSprite;
	public Sprite openSprite;
	private Image LightImage;
	private Toggle ThermaVisonTogglo;
	private Toggle NightVisionToggle;
	private Toggle FlashLight;
	public bool isOpen = false;
	private Toggle tempToggle;

	// Use this for initialization
	void Start () {
		LightImage = this.GetComponent<Image> ();
		ThermaVisonTogglo = GameObject.Find ("Canvas/RealStrikeOptionPanel/Scroll Snap Vertical Effect/List/ThermaVision").transform.GetComponent<Toggle> ();
		NightVisionToggle = GameObject.Find ("Canvas/RealStrikeOptionPanel/Scroll Snap Vertical Effect/List/NightVisionToggle").transform.GetComponent<Toggle> ();
		FlashLight = GameObject.Find ("Canvas/RealStrikeOptionPanel/Scroll Snap Vertical Effect/List/FlashLight").transform.GetComponent<Toggle> ();
		tempToggle = null;
	}
	
	public void OnClick(){
		if (isOpen) {
			if (ThermaVisonTogglo.isOn) {
				ThermaVisonTogglo.isOn = false;
				LightImage.sprite = closeSprite;
				ThermaVisonTogglo.onValueChanged.Invoke (false);
				tempToggle = ThermaVisonTogglo;
			}

			if (NightVisionToggle.isOn) {
				NightVisionToggle.isOn = false;
				LightImage.sprite = closeSprite;
				NightVisionToggle.onValueChanged.Invoke (false);
				tempToggle = NightVisionToggle;
			}

			if (FlashLight.isOn) {
				FlashLight.isOn = false;
				LightImage.sprite = closeSprite;
				FlashLight.onValueChanged.Invoke (false);
				tempToggle = FlashLight;
			}

			isOpen = false;
		} else {
			if (tempToggle == null)
				return;
			tempToggle.isOn = true;
			LightImage.sprite = openSprite;
			tempToggle.onValueChanged.Invoke (true);
		}

	}

	public void OnOpenSprit(){
		if (NightVisionToggle.isOn || ThermaVisonTogglo.isOn || FlashLight.isOn) {
			LightImage.sprite = openSprite;
			isOpen = true;
		}

		if (!NightVisionToggle.isOn && !ThermaVisonTogglo.isOn && !FlashLight.isOn) {
			LightImage.sprite = closeSprite;
			isOpen = false;
		}
	}
}
