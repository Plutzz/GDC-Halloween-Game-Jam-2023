using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyHomingBullet : MonoBehaviour
{
    public float force;
    public int damage;

    Vector3 direction;

    private GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start ()
    {
        player = PlayerMovement.Instance.gameObject;
    }

    void Update()
    {

        direction = player.transform.position - transform.position;

        float distance = force * Time.deltaTime;

        transform.Translate(direction.normalized * distance, Space.World);

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
