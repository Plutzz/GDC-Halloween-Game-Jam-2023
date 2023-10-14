using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.Instance.path[pathIndex];
    }

    private void Update()
    {
        //checks distance to next path node
        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            //If at last node, destroy enemy
            if(pathIndex == LevelManager.Instance.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            //else set target to next node
            else
            {
                target = LevelManager.Instance.path[pathIndex];
            }

        }

    }

    private void FixedUpdate()
    {
        //Move towards target 
        Vector2 _direction = (target.position - transform.position).normalized;
        rb.velocity = _direction * moveSpeed;
    }
}
