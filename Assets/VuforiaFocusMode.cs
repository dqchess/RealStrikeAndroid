using UnityEngine;
using System.Collections;
using Vuforia;

public class VuforiaFocusMode : MonoBehaviour {
    public CameraDevice.FocusMode FocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;
    void Start()
    {
		VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
    }
    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(FocusMode);
    }

    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(FocusMode);
        }
    }
}
