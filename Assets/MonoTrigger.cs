using UnityEngine;
using System.Collections;

public class MonoTrigger : MonoBehaviour {
    public SimpleEvent onStart;
	// Use this for initialization
	void Start () {
        if (onStart != null)
            onStart.Invoke();

    }
}
