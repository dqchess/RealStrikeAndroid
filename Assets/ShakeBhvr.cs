using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CameraFilterPack_Blur_Dithering2x2))]
public class ShakeBhvr : MonoBehaviour {
    [HideInInspector]
    public float range;
    // Use this for initialization
    void Start() {

    }
    public void Shake() {
        StartCoroutine(ShakeCoroutine());
    }
    IEnumerator ShakeCoroutine() {
        var x = Random.Range(0, range);
        var y = range - x;
        var ditherBhvr = GetComponent<CameraFilterPack_Blur_Dithering2x2>();
        ditherBhvr.IsPause = false;
        yield return 0;
        yield return 0;
        ditherBhvr.IsPause = true;

    }
}
