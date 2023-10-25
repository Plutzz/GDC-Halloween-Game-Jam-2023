using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public Transform StartPoint;
    public Transform[] path;
    public Transform[] surround;

    public int Currency;
    public int maxMana = 500;
    public int manaPerSecond = 5;
    public int currentMana;
    private int manaRate = 1;
    private float manaCooldown = 0;
    public UnityEngine.UI.Image mana;


    private void Start()
    {
        Currency = 1000;
        currentMana = maxMana;
    }

    void Update ()
    {
        if(currentMana < maxMana)
        {
            ManaRegen();
            float fillvalue = (float)currentMana/(float)maxMana;

            mana.fillAmount = fillvalue;
        }
    }

    public void IncreaseCurrency(int amount)
    {
        Currency += amount;
    }

    public bool SpendCurrency(int amount) 
    {
        if(amount <= Currency) 
        {
            //Buy Item
            Currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Insufficent Currency");
            return false;
        }
    }

    public void ManaRegen ()
    {
        manaCooldown -= Time.deltaTime;

        if(manaCooldown <= 0)
        {
            currentMana = Mathf.Clamp(currentMana + manaPerSecond, 0, maxMana);
            Debug.Log(currentMana);
            manaCooldown = manaRate;
        }
    }

    public bool SpendMana(int amount) 
    {
        if(amount <= currentMana) 
        {
            //Buy Item
            currentMana -= amount;
        
            float fillvalue = (float)currentMana/(float)maxMana;

            mana.fillAmount = fillvalue;
            return true;
        }
        else
        {
            Debug.Log("Insufficent Mana");
            return false;
        }
    }
}
