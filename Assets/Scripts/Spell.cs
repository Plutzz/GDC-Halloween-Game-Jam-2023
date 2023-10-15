using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject clearSpell;
    public int costOfSpell = 200;
    public int spellCooldown = 1;
    private float cooldown = 1;
    private bool spellReady = true;

    private bool buildModeLastFrame = false;

    void Update()
    {
        if (!spellReady)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                spellReady = true;
            }
        }

        if (Input.GetMouseButtonDown(1) && spellReady && !buildModeLastFrame)
        {
            if(LevelManager.Instance.currentMana >= costOfSpell)
            {
                Lightning.Instance.Flash();
                Instantiate(clearSpell, transform.position, transform.rotation);
                LevelManager.Instance.SpendMana(costOfSpell);
                cooldown = spellCooldown;
                spellReady = false;
            } else {
                Debug.Log("Not enough mana");
            }
        }
        buildModeLastFrame = GridBuildingSystem.Instance.buildModeEnabled;
    }
}
