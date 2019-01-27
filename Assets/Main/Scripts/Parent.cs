

using UnityEngine;

public class Parent : MonoBehaviour
{
    public static readonly Vector4 BLACK = new Vector4(0, 0, 0, 1);

    public FlameState state = FlameState.LIGHT_DOWN;
    public GameObject flamePivot;
    public bool active = false;
    public Flame flame;
    public SpriteRenderer shine;
    public GameObject shineGO;
    public GameObject glowGO;
    public float timer;
    public float lifeSpanTimer;

    public void ResetFlame() {
        shine.color = BLACK;
        timer = 0;
        lifeSpanTimer = 0;
        flame.percentage = 0;
        flamePivot.SetActive(false);
        shineGO.SetActive(false);
        glowGO.SetActive(false);
        flame.gameObject.SetActive(false);
    }

    public void Activate() {
        flamePivot.SetActive(true);
        shineGO.SetActive(true);
        glowGO.SetActive(true);
        flame.gameObject.SetActive(true);
        state = FlameState.LIGHT_DOWN;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(flamePivot.transform.localPosition, transform.localScale);
    }
}

public enum FlameState {
    OFF,
    LIGHT_DOWN,
    FIRE_ON,
    OH_NO,
    CUTSCENE_ACTIVE
}