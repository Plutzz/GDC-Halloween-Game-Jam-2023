using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraightBullet : MonoBehaviour
{
    public float force;
    public int damage;

    private GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * force;
        //transform.rotation = Quaternion.Euler(0, 0, 90);
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}