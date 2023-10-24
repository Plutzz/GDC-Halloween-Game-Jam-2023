using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Spell : MonoBehaviour
{
    public GameObject clearSpell;
    public GameObject freezeSpell;

    public int costOfSpellClearSpell = 100;
    public int costOfFreezeSpell = 70;
    public int spellCooldown = 1;
    private float cooldown = 1;
    private bool spellReady = true;

    private bool buildModeLastFrame = false;
    [SerializeField]
    private SpellType spellType = SpellType.Clear;

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

        switch((int)spellType)
        {
            case(0):
            {
                if (Input.GetMouseButtonDown(1) && spellReady && !buildModeLastFrame)
                {
                    if(LevelManager.Instance.currentMana >= costOfSpellClearSpell)
                    {
                        Lightning.Instance.Flash();
                        Instantiate(clearSpell, transform.position, transform.rotation);
                        LevelManager.Instance.SpendMana(costOfSpellClearSpell);
                        cooldown = spellCooldown;
                        spellReady = false;
                    } else {
                        Debug.Log("Not enough mana");
                    }
                }

                buildModeLastFrame = GridBuildingSystem.Instance.buildModeEnabled;

                break;
            }

            case(1):
            {
                if (Input.GetMouseButtonDown(1) && spellReady && !buildModeLastFrame)
                {
                    if(LevelManager.Instance.currentMana >= costOfFreezeSpell)
                    {
                        Lightning.Instance.Flash();
                        Instantiate(freezeSpell, transform.position, transform.rotation);
                        LevelManager.Instance.SpendMana(costOfFreezeSpell);
                        cooldown = spellCooldown;
                        spellReady = false;
                    } else {
                        Debug.Log("Not enough mana");
                    }
                }

                buildModeLastFrame = GridBuildingSystem.Instance.buildModeEnabled;

                break;
            }
        }
    }
}

enum SpellType
{
    Clear,
    Freeze,
}