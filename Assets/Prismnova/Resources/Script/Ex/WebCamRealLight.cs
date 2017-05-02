using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Light))]
public class WebCamRealLight : MonoBehaviour {
    float grayscales;
    int index;
    private Light _light = null;
    private WebCamBhvr _cam = null;
    void Start() {
        StartCoroutine(Init());
    }
    IEnumerator Init() {
        yield return new WaitForSeconds(2f);
        _cam = FindObjectOfType<WebCamBhvr>();
        _light = GetComponent<Light>();
    }
    void OnDestroy()
    {
        _cam = null;
        _light = null;
    }
    void LateUpdate()
    {
        if (_cam == null || _light == null)
            return;
        if (!_cam.didUpdateThisFrame)
            return;
        var size = _cam.Size;
        if (size == Vector2.zero)
            return;
        for (int x = 0; x < size.x; x+=500)
        {
            for (int  y= 0;  y < size.y; y+=100)
            {
                var color = _cam.GetPixel(x, y);
                grayscales += color.grayscale;
                index++;
            }
        }
        _light.intensity = grayscales / index;
        grayscales = 0;
        index = 0;
    }
}
