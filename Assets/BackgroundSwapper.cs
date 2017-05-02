using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class BackgroundSwapper : MonoBehaviour {

    public Sprite[] sprites;
    public int index;
    public void Next() {
        index++;
        if (sprites.Length <= index)
            index = 0;
        GetComponent<Image>().sprite = sprites[index];
    }
}
