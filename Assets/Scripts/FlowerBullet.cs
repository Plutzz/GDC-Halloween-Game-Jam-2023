using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime = 1f;

    private float timeAlive = 0;

    private Vector2 direction;

    private Transform target;

    private void Start()
    {
        if (target == null) return;

    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;

        timeAlive += Time.deltaTime;
        if(timeAlive > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Take Health From Enemy
        collision.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);

        Destroy(gameObject);
    }
}
