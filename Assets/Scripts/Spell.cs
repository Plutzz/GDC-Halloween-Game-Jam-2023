using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
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
                LevelManager.Instance.SpendMana(costOfSpell);
            } else {
                Debug.Log("Not enough mana");
            }
        }
    }
}
