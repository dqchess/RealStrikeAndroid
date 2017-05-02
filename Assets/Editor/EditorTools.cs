using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using I2.Loc;

public class EditorTools : Editor
{
    [MenuItem("Tools/PlayerPrefs/DeleteAll")]
    static void PlayerPrefsDeleteAll() {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clean All Prefs");
    }
    [MenuItem("Tools/Localization/Language/English")]
    static void LocalizationLanguageEnglish()
    {
        LocalizationManager.CurrentLanguage = "english";
    }
    [MenuItem("Tools/Localization/Language/Chinese")]
    static void LocalizationLanguageChinese()
    {
        LocalizationManager.CurrentLanguage = "chinese";
    }
}
