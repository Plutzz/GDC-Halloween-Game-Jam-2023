using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiralBullet : MonoBehaviour
{
    public float force;
    public int damage;
    public float rotationRate = 80;

    private GameObject player;
    private Vector3 direction;
    private Vector3 playerPosition;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerMovement.Instance.gameObject;
        playerPosition = player.transform.position;
        Destroy(this.gameObject, 10f);
    }

    void Update()
    {
        direction = playerPosition - transform.position;

        direction = Quaternion.Euler(0, 0, rotationRate) * direction;

        float distanceThisFrame = force * Time.deltaTime;
            
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
