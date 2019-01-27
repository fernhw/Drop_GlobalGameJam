using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleType type = ObstacleType.CUBE;
    private void OnDrawGizmos() {
        Gizmos.color = Color.gray;
        if (type == ObstacleType.CUBE) {
            Gizmos.DrawWireCube(transform.localPosition, transform.localScale);
        } else {
            Gizmos.DrawWireSphere(transform.localPosition, transform.localScale.x);
        }
    }
}
public enum ObstacleType {
    CUBE = 0,
    SPHERE = 1
}