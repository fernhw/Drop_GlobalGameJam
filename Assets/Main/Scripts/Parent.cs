

using UnityEngine;

public class Parent : MonoBehaviour
{
    public ParentState state = ParentState.SMALL;

    public bool active = false;

    private void Start() {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.localPosition, transform.localScale);
    }
}



public enum ParentState {
    SMALL,
    MEDIUM,
    OH_NO
}