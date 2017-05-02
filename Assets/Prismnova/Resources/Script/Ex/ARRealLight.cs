using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
[RequireComponent(typeof(Light))]
public class ARRealLight : MonoBehaviour {
    float grayscales;
    int count;
    protected Light mLight {
        get {
            return GetComponent<Light>();
        }
    }

	Image.PIXEL_FORMAT m_PixelFormat = Image.PIXEL_FORMAT.GRAYSCALE;
    private bool m_RegisteredFormat = false;
    private bool m_LogInfo = true;

    void Start()
    {
		VuforiaARController vuforiaBehaviour = VuforiaARController.Instance;
		if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
        }
    }
    public void OnTrackablesUpdated()
    {
        if (!m_RegisteredFormat)
        {
            CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);
            m_RegisteredFormat = true;
        }
        if (m_LogInfo)
        {
            CameraDevice cam = CameraDevice.Instance;
            Image image = cam.GetCameraImage(m_PixelFormat);
            if (image == null)
            {
                Debug.Log(m_PixelFormat + " image is not available yet");
            }
            else
            {
                for (int x = 0; x < image.Width; x += 500)
                {
                    for (int y = 0; y < image.Height; y += 100)
                    {
                        grayscales += image.Pixels[x * y];
                        count++;
                    }
                }
            }
            mLight.intensity = grayscales / count / 255 * 1.5f;
        }
		grayscales = 0;
		count = 0;
    }
}