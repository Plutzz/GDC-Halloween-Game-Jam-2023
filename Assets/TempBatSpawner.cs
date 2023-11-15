using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBatSpawner : MonoBehaviour
{
    public int spawnTimer = 2;
    public bool spawnReady = true;
    public GameObject batPrefab;
    void Update()
    {
        StartCoroutine(BatSpawn());
    }

    private IEnumerator BatSpawn()
    {
        if(spawnReady)
        {
            spawnReady = false;
            yield return new WaitForSeconds(spawnTimer);
            Instantiate(batPrefab, transform.position, transform.rotation);
            spawnReady = true;
        }
    }
}
