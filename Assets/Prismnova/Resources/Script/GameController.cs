using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Vuforia;
using System;

public class GameController : MonoBehaviour
{
    // Use this for initialization
    void Start () {
        MobileNative.debugLog = true;
    }

    
}

[Serializable]
public class SimpleEvent : UnityEvent { }
[Serializable]
public class BoolEvent : UnityEvent<bool> { }
[Serializable]
public class StringEvent : UnityEvent<string> { }