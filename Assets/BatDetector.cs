using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDetector : MonoBehaviour
{
    public RoomSides roomSide;
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            AlternateEnemyEndBehavior enemyEndBehavior = other.GetComponent<AlternateEnemyEndBehavior>();
            switch(roomSide)
            {
                case(RoomSides.Right):
                    enemyEndBehavior.SetWayPoint(0);
                    if(enemyEndBehavior.transform.position.x > transform.position.x)
                    {
                        enemyEndBehavior.ResetCooldown();
                    }
                break;

                case(RoomSides.Top):
                    enemyEndBehavior.SetWayPoint(1);
                    if(enemyEndBehavior.transform.position.y > transform.position.y)
                    {
                        enemyEndBehavior.ResetCooldown();
                    }
                break;

                case(RoomSides.Left):
                    enemyEndBehavior.SetWayPoint(2);
                    if(enemyEndBehavior.transform.position.x < transform.position.x)
                    {
                        enemyEndBehavior.ResetCooldown();
                    }
                break;
                
                case(RoomSides.Bot):
                    enemyEndBehavior.SetWayPoint(3);
                    if(enemyEndBehavior.transform.position.y < transform.position.y)
                    {
                        enemyEndBehavior.ResetCooldown();
                    }
                break;
            }
        }
    }

    public enum RoomSides
    {
        Right,
        Top,
        Left,
        Bot
    }
}
