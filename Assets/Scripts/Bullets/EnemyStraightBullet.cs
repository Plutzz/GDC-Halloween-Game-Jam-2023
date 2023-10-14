using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Destroy(this.gameObject, 5f);
    }
    
    private void OnTriggerEnter2D (Collider2D collider) {
        if(collider.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}