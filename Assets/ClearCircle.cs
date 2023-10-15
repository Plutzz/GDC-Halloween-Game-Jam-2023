using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.5f);   
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            //has to reduce enemies in enemyspawner to get next wave, make enemy spawner singleton?
        }
    }
}
