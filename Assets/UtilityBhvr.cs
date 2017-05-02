using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class UtilityBhvr : MonoBehaviour {


    public void SaveToggleStatus(Toggle tgl) {
        if (tgl == null)
            return;
        PlayerPrefs.SetString(GetFullName(tgl),tgl.isOn.ToString());
    }

    public void RestoreToggleStatus(Toggle tgl)
    {
        if (tgl == null)
            return;
        var val = PlayerPrefs.GetString(GetFullName(tgl));
        if (string.IsNullOrEmpty(val))
            return;
        tgl.isOn = bool.Parse(val);
    }

    public static string GetFullName(Component c) {
        if (c == null)
            return default(string);
        List<string> names = new List<string>();
        Transform trans = c.transform;
        while (true) {
            names.Add(trans.name);
            if (trans.parent == null)
                break;
            trans = trans.parent;
        }
        names.Reverse();
        return string.Join("/", names.ToArray());
    }
    
}
