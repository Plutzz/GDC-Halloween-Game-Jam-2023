using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class EnemyEndBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Renderer sr;
    //public float angle = 0f;

    public Vector3 centerOfRotation = new(-15, 5, 0);
    public GameObject bullet;
    public bool end = false;
    //public float rotationRadius = 2f;
    public float speed = 2f;
    public float fireRate = 1f;
    public float fireCooldown = 3f;
    //public float timeBeforeRotation = 1;
    //public float rotationTimer = 1;

    private Transform wayPoint;
    private int surroundIndex = 0;

    void Start ()
    {
        wayPoint = LevelManager.Instance.surround[surroundIndex];
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<Renderer>();
    }

    void Update ()
    {
        if(end)
        {
            //CirclePlayer();

            fireCooldown -= Time.deltaTime;
            LookAt2D(transform, new Vector2(centerOfRotation.x, centerOfRotation.y));

            if(fireCooldown <= 0)
            {
                Instantiate(bullet, transform.position, transform.localRotation);
                fireCooldown = fireRate;
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
                //set target to next node
                wayPoint = LevelManager.Instance.surround[surroundIndex];

            }
        }
    }

    private void FixedUpdate()
    {
        if(end)
        {
            //Move towards target 
            Vector2 _direction = (wayPoint.position - transform.position).normalized;
            rb.velocity = _direction * speed;
        }
    }

    public void ReachedEnd ()
    {
        gameObject.layer = LayerMask.NameToLayer("Projectiles");
        sr.sortingLayerID = SortingLayer.NameToID("Projectiles");
    }

    // private void CirclePlayer()
    // {

    //     angle += Time.deltaTime * speed;
    //     Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
    //     transform.position = centerOfRotation + direction * rotationRadius;
    //     LookAt2D(transform, new Vector2(centerOfRotation.x, centerOfRotation.y));

    //     if(angle >= 360f)
    //     {
    //         angle = 0f;
    //     }

    //     if(angle >= 0 && angle <= 180)
    //     {
    //         fireCooldown -= Time.deltaTime;
    //     }

    //     if(fireCooldown <= 0)
    //     {
    //         Instantiate(bullet, transform.position, transform.localRotation);
    //         fireCooldown = fireRate;
    //     }
    // }

    // private void SquarePlayer()
    // {
    //     transform.position += transform.up * speed * Time.deltaTime;
    //     Debug.Log("moving right");

    //     rotationTimer -= Time.deltaTime;
        
    //     if(rotationTimer <= 0)
    //     {
    //         transform.Rotate(new Vector3(0, 0, 90));
    //         rotationTimer = timeBeforeRotation;
    //     }
    // }

    public void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        var direction = target - current;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
