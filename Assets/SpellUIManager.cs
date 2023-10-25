using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUIManager : MonoBehaviour
{
    public Transform center;
    public Transform selectObject;

    public GameObject RadialMenuRoot;
    public bool isRadialMenuActive = false;

    public TextMeshProUGUI selectedSpellName;

    public int amountOfSpells = 9;
    public string[] spellNames;
    private float spellSpace = 0;


    void Start ()
    {
        spellSpace = 360 / amountOfSpells;
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RadialMenuRoot.transform.position = Input.mousePosition;
            RadialMenuRoot.SetActive(true);
            isRadialMenuActive = true;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            RadialMenuRoot.SetActive(false);
            isRadialMenuActive = false;
        }

        if(isRadialMenuActive)
        {
            Vector2 delta = center.position - Input.mousePosition;
            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            angle += 180;

            int currentSpell = 0;

            for(float i = 0; i < 360; i += spellSpace)
            {
                if(angle >= i && angle < i + spellSpace)
                {
                    selectObject.eulerAngles = new Vector3(0, 0, i);
                    selectedSpellName.text = spellNames[currentSpell];
                    Spell.Instance.ChangeSpell(currentSpell);
                }

                currentSpell++;
            }
        }

    }
}
