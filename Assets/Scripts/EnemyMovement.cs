using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] protected float moveSpeed = 2f;
    protected float currentMoveSpeed;

    [SerializeField] private EnemyEndBehavior endBehavior;
    public Animator Anim;
    public float distanceErrorMargin = 0f;
    public float freezeTime = 3f;
    protected float freezeTimer;

    protected Transform target;
    protected int pathIndex = 0;
    protected bool frozen = false;

    private void Start()
    {
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
                endBehavior.ReachedEnd();
                endBehavior.end = true;
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

    private void FixedUpdate()
    {
        //Move towards target 
        Vector2 _direction = (target.position - transform.position).normalized;
        rb.velocity = _direction * currentMoveSpeed;

        Anim.SetFloat("XSpeed", Mathf.Abs(rb.velocity.x));
        Anim.SetFloat("YVelocity", rb.velocity.y);
    }

    public void Freeze ()
    {
        freezeTimer = freezeTime;
        frozen = true;
    }
}
