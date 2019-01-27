using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public List<GameObject> flames;
    [Range(0,1)]
    public float percentage = 0;
    public SpriteRenderer glow;

    public void ShowFlames() {
        int flameCount = flames.Count;
        int flamesToShow = Mathf.RoundToInt(flameCount*percentage);
        for (int i = 0; i<flameCount; i++) {
            GameObject flame = flames[i];
            if (i<flamesToShow) {
                flame.SetActive(true);
            } else {
                flame.SetActive(false);
            }
        }
        glow.color = new Vector4(percentage, percentage, percentage,1);
    }

    private void Update() {
        ShowFlames();
    }
}
