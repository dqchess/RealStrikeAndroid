using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class LayoutText : LayoutElement {
    Text mText;
    protected override void Start()
    {
        base.Awake();
        mText = GetComponent<Text>();
        minHeight = mText.minHeight;
    }
}
