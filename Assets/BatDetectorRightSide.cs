using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDetectorRightSide : MonoBehaviour
{
    public int index = 0;
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            AlternateEnemyEndBehavior enemyEndBehavior = other.GetComponent<AlternateEnemyEndBehavior>();
            enemyEndBehavior.SetWayPoint(index);
            if(enemyEndBehavior.transform.position.x > transform.position.x)
            {
                enemyEndBehavior.ResetCooldown();
            }
        }
    }
}
