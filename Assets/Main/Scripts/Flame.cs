using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    const float
         REAL_PERCENTAGE = .5f,
         FLAME_ADJUST_SPEED = 1f;
    public List<GameObject> flames;
    [Range(0,1)]

    public float percentage = 0;
    public SpriteRenderer glow;
   
    public float actualFlame = 0;

    public void ShowFlames(float delta) {
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
        if (actualFlame>percentage*REAL_PERCENTAGE) {
            actualFlame -= FLAME_ADJUST_SPEED*delta;
        }
        if (percentage*REAL_PERCENTAGE>actualFlame) {
            actualFlame = percentage*REAL_PERCENTAGE;
        }
        glow.color = new Vector4(actualFlame, actualFlame, actualFlame, 1);
    }

}
