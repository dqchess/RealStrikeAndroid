using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateAnimationClip : AssetPostprocessor
{
    void OnPreprocessAnimation()
    {
        var modelImporter = assetImporter as ModelImporter;
        if (modelImporter.clipAnimations.Length == 0)
        {
            modelImporter.clipAnimations = modelImporter.defaultClipAnimations;
        }
    }
}