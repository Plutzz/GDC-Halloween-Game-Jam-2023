using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Transform StartPoint;
    public Transform[] path;

    public int Currency;

    private void Start()
    {
        Currency = 500;
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
}
