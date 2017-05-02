using UnityEngine;
using System.Collections;

public class RecordBhvr : MonoBehaviour {

    public SimpleEvent onRecordStarted;
    public SimpleEvent onRecordStoped;
    // Use this for initialization
    void Start () {
        AddEventlistener();
        if (ISN_ReplayKit.Instance.IsRecording)
        {
            if (onRecordStarted != null)
                onRecordStarted.Invoke();
        }

    }


    void AddEventlistener()
    {
        ISN_ReplayKit.ActionRecordStoped += HandleActionRecordStoped;
        ISN_ReplayKit.ActionRecordStarted += HandleActionRecordStarted;
        ISN_ReplayKit.ActionRecordInterrupted += HandleActionRecordInterrupted;
		ISN_ReplayKit.ActionShareDialogFinished += HandleActionShareDialogFinished;

    }
    void RemoveEventlistener() {
        ISN_ReplayKit.ActionRecordStoped -= HandleActionRecordStoped;
        ISN_ReplayKit.ActionRecordStarted -= HandleActionRecordStarted;
        ISN_ReplayKit.ActionRecordInterrupted -= HandleActionRecordInterrupted;
        ISN_ReplayKit.ActionShareDialogFinished -= HandleActionShareDialogFinished;
    }


    public void StartRecording()
    {
        bool microphoneEnabled = true;
        ISN_ReplayKit.Instance.StartRecording(microphoneEnabled);
#if UNITY_EDITOR
        if (onRecordStarted != null)
            onRecordStarted.Invoke();
#endif
    }

    public void StopRecording()
    {
        ISN_ReplayKit.Instance.StopRecording();
#if UNITY_EDITOR
        if (onRecordStoped != null)
            onRecordStoped.Invoke();
#endif
    }

    public void OnDestroy()
    {
        RemoveEventlistener();
#if UNITY_EDITOR
        //Nothing
#else
        if (ISN_ReplayKit.Instance.IsRecording)
            ISN_ReplayKit.Instance.StopRecording();
#endif
    }

    private void HandleActionRecordStarted(SA.Common.Models.Result res)
    {
        if (res.IsSucceeded)
        {
            //MobileNative.Alert("Record was sucssesfully started");

			if (onRecordStarted != null)
				onRecordStarted.Invoke ();
        }
        else
        {
            MobileNative.Alert("Record start failed: " + res.Error.Message);
        }
    }

    private void HandleActionRecordStoped(SA.Common.Models.Result res)
    {
        if (res.IsSucceeded)
        {
            //MobileNative.Alert("Record was sucssesfully stopped");
			if (onRecordStoped != null)
				onRecordStoped .Invoke();
			ISN_ReplayKit.Instance.ShowVideoShareDialog();
        }
        else
        {
            MobileNative.Alert("Record stop failed: " + res.Error.Message);
        }
    }


    void HandleActionRecordInterrupted(SA.Common.Models.Error error)
    {
        MobileNative.Alert("Video was interrupted with error: " + error.Message);
    }



	void HandleActionShareDialogFinished (ReplayKitVideoShareResult res) {
		if(res.Sources.Length > 0) {
			foreach(string source in res.Sources) {
				MobileNative.Alert ("Success: User has shared the video to:" + source);
			}
		} else {
			MobileNative.Alert ("Fail: User declined video sharing!");
		}
	}
}
