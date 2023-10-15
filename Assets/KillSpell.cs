using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSpell : MonoBehaviour
{
    public GameObject clearSpell;
    public int costOfSpell = 300;

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(LevelManager.Instance.currentMana >= costOfSpell)
            {
                Instantiate(clearSpell, transform.position, transform.rotation);
                LevelManager.Instance.currentMana -= costOfSpell;
            } else {
                Debug.Log("Not enough mana");
            }
        }
    }
}
