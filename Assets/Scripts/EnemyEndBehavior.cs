using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class EnemyEndBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Renderer sr;
    public float angle = 0f;

    public Vector3 centerOfRotation = new(-15, 5, 0);
    public GameObject bullet;
    public bool end = false;
    public float rotationRadius = 2f;
    public float angularSpeed = 2f;
    public float fireRate = 1f;
    public float fireCooldown = 3f;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<Renderer>();
    }

    void Update ()
    {
        CirclePlayer();
    }

    public void ReachedEnd ()
    {
        gameObject.layer = LayerMask.NameToLayer("Projectiles");
        sr.sortingLayerID = SortingLayer.NameToID("Projectiles");
    }

    private void CirclePlayer()
    {
        if(end)
        {
            angle += Time.deltaTime * angularSpeed;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
            transform.position = centerOfRotation + direction * rotationRadius;
            LookAt2D(transform, new Vector2(centerOfRotation.x, centerOfRotation.y));

            if(angle >= 360f)
            {
                angle = 0f;
            }

            if(angle >= 0 && angle <= 180)
            {
                fireCooldown -= Time.deltaTime;
            }

            if(fireCooldown <= 0)
            {
                Instantiate(bullet, transform.position, transform.localRotation);
                fireCooldown = fireRate;
            }
        }
    }

    public void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        var direction = target - current;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
