using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraightBullet : MonoBehaviour
{
    public float force;
    public int damage;

    private Vector3 turn90 = new(0, 0, -90);
    private GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(turn90);
        rb.velocity = transform.up * force;
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}