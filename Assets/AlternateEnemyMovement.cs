using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateEnemyMovement : EnemyMovement
{

    [SerializeField] private AlternateEnemyEndBehavior alternateEndBehavior;

    private void Start()
    {
        pathIndex = LevelManager.Instance.path.Length - 2;   
        target = LevelManager.Instance.path[pathIndex];
        currentMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if(frozen)
        {
            currentMoveSpeed = 0;
            freezeTimer -= Time.deltaTime;

            if(freezeTimer <= 0)
            {
                frozen = false;
            }
        } else if (!frozen)
        {
            currentMoveSpeed = moveSpeed;
        }

        //checks distance to next path node
        if (Vector2.Distance(target.position, transform.position) <= distanceErrorMargin)
        {
            pathIndex++;

            //If at last node, destroy enemy
            if(pathIndex == LevelManager.Instance.path.Length)
            {
                rb.velocity = Vector2.zero;
                EnemySpawner.onEnemyDestroy.Invoke();
                alternateEndBehavior.ReachedEnd();
                alternateEndBehavior.end = true;
                Destroy(this);
                return;
            }
            //else set target to next node
            else
            {
                target = LevelManager.Instance.path[pathIndex];
            }

        }

    }
}
