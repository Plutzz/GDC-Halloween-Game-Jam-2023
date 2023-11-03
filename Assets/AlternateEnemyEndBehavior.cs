using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateEnemyEndBehavior : MonoBehaviour
{

    private Rigidbody2D rb;
    private Renderer sr;
    //public float angle = 0f;

    public bool spotted = false;
    public bool end = false;
    //public float rotationRadius = 2f;
    public float speed = 2f;
    public float chargeSpeed = 4f;
    public float tackleRate = 5f;
    public float tackleCooldown = 0f;
    //public float timeBeforeRotation = 1;
    //public float rotationTimer = 1;

    private Transform wayPoint;
    private int surroundIndex = 0;
    private bool tackling = false;

    void Start ()
    {
        tackleCooldown = tackleRate;
        wayPoint = LevelManager.Instance.surround[surroundIndex];
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<Renderer>();
    }

    void Update ()
    {
        if(end)
        {
            //CirclePlayer();

            if(tackleCooldown <= 0 && !tackling)
            {
                tackling = true;
                LookAt2D(transform, new Vector2(PlayerMovement.Instance.transform.position.x, PlayerMovement.Instance.transform.position.y));
            }else if (tackleCooldown <= 0 && tackling)
            {
                ChargePlayer();
            }
        
            //checks distance to next path node
            if (Vector2.Distance(wayPoint.position, transform.position) <= 0.1f)
            {
                surroundIndex++;

                //If at last node, reset nodes
                if(surroundIndex == LevelManager.Instance.surround.Length)
                {
                    surroundIndex = 0;
                }
                SetWayPoint(surroundIndex);

            }
        }
    }

    private void FixedUpdate()
    {
        if(end && !tackling)
        {
            //Move towards target 
            Vector2 _direction = (wayPoint.position - transform.position).normalized;
            rb.velocity = _direction * speed;
            tackleCooldown -= Time.deltaTime;
            Debug.Log(wayPoint);
        } else if (tackling)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ChargePlayer ()
    {
        transform.position += transform.right * chargeSpeed * Time.deltaTime;
    }

    public void ReachedEnd()
    {
        gameObject.layer = LayerMask.NameToLayer("Projectiles");
        sr.sortingLayerID = SortingLayer.NameToID("Projectiles");
    }
    private void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        var direction = target - current;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetIndex (int index)
    {
        if(tackling)
        {
            surroundIndex = index;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().takeDamage(10);
        }
    }

    public void ResetCooldown()
    {
        tackleCooldown = tackleRate;
        tackling = false;
    }

    public void SetWayPoint(int index)
    {
        //set target to next node
        wayPoint = LevelManager.Instance.surround[index];
        surroundIndex = index;
    }
}
