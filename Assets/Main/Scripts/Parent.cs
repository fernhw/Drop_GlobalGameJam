

using UnityEngine;

public class Parent : MonoBehaviour
{
    public ParentState state = ParentState.SMALL;
    public GameObject flamePivot;
    public bool active = false;
    public Flame flame;
    private void Start() {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(flamePivot.transform.localPosition, transform.localScale);
    }
}



public enum ParentState {
    SMALL,
    MEDIUM,
    OH_NO
}