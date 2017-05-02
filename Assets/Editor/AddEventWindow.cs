using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class AddEventWindow : EditorWindow
{
    [MenuItem("Window/AddEventWindow")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AddEventWindow));
    }
    string myString = "";
    OPTIONS mFunctionOption = OPTIONS.OnFire;
    void OnGUI()
    {
        myString = EditorGUILayout.TextArea(myString);
        if (GUILayout.Button("OnFire")) {
            mFunctionOption = OPTIONS.OnFire;
            Execute();
        };
        if (GUILayout.Button("PlayAudio"))
        {
            mFunctionOption = OPTIONS.PlayAudio;
            Execute();
        };
        //if (GUILayout.Button("OnReloadComplete"))
        //{
        //    mFunctionOption = OPTIONS.OnReloadComplete;
        //    Execute();
        //};

        if (GUILayout.Button("Clean"))
        {
            mFunctionOption = OPTIONS.Clean;
            Execute();
        };


    }

    void Execute() {
        var o = Selection.activeObject;
        string path = AssetDatabase.GetAssetPath(o);
        var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
        if (clip == null)
            return;
        List<AnimationEvent> eventList = new List<AnimationEvent>();

        switch (mFunctionOption)
        {
            case OPTIONS.PlayAudio:
                var rows = myString.Split('\n');
                foreach (var row in rows)
                {
                    var args = row.Split(' ');
                    var frame = 0;
                    var stringParameter = "";
                    if (args.Length > 1)
                    {
                        frame = int.Parse(args[0]);
                        stringParameter = args[1];
                    }
                    else {
                        stringParameter = args[0];
                    }
                    var et = GetEvent(frame / 30f / clip.length, mFunctionOption.ToString(), stringParameter);
                    eventList.Add(et);
                }
                if (path.ToLower().EndsWith("reload.fbx")) {
                    var onReloadCompleteet1 = GetEvent(0.95f, OPTIONS.OnReloadComplete.ToString(), "");
                eventList.Add(onReloadCompleteet1);
                }
                break;
            case OPTIONS.OnFire:
                var fireet = GetEvent(0, mFunctionOption.ToString(), "");
                eventList.Add(fireet);
                var playaudioet = GetEvent(0, OPTIONS.PlayAudio.ToString(), "fire");
                eventList.Add(playaudioet);
                break;
            case OPTIONS.Clean:
                break;
            case OPTIONS.OnReloadComplete:
                var onReloadCompleteet = GetEvent(0.95f, mFunctionOption.ToString(), "");
                eventList.Add(onReloadCompleteet);
                break;
            default:
                break;
        }
        AddEvent.DoSetEventImportedClip(eventList.ToArray(), clip);
        Debug.Log("Build Complete !!!");
    }

    
    public enum OPTIONS {
        PlayAudio,
        OnFire,
        OnReloadComplete,
        Clean
    }
    public AnimationEvent GetEvent(float time,string functionName, string stringParameter)
    {
        List<string> lst = new List<string>();
        lst.Add("\n");
        lst.Add("\t");
        lst.Add("\r");
        lst.Add(".wav");
        lst.Add(".mp3");
        lst.Add(".ogg");
        foreach (var v in lst) stringParameter = stringParameter.Replace(v, "");
        var et = new AnimationEvent();
        et.time = Mathf.Clamp(time, 0, 1);
        et.functionName = functionName;
        et.stringParameter = stringParameter;
        return et;
    }

}
