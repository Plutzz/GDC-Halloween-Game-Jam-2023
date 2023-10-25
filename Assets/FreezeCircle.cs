using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreezeCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.5f);   
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovementScript))
            {
                enemyMovementScript.Freeze();
            }
            
        }
    }
}
