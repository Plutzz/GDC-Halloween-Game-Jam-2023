using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class EnemyEndBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Renderer sr;
    public Vector3 centerOfRoom = new(0, 0, 0);

    public GameObject bullet;
    public bool end = false;
    public float rotationRadius = 2f;
    public float angularSpeed = 2f;
    public float angle = 0f;
    public float fireRate = 0f;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<Renderer>();
    }

    void Update ()
    {
        CirclePlayer();
        // if(fireRate <= 0)
        // {
        //     Instantiate(bullet);
        // }
    }

    public void ReachedEnd ()
    {
        gameObject.layer = LayerMask.NameToLayer("Projectiles");
        sr.sortingLayerID = SortingLayer.NameToID("Projectiles");
        Debug.Log("enemy is in projectile layer");
    }

    private void CirclePlayer()
    {
        if(end)
        {
            angle += Time.deltaTime * angularSpeed;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
            transform.position = centerOfRoom + direction * rotationRadius;
            transform.LookAt(centerOfRoom);

            if(angle >= 360f)
            {
                angle = 0f;
            }
        }
    }
}
