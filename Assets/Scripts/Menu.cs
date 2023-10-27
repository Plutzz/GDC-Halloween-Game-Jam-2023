using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;

    // INDEX KEY:
    // 0 - Cannon
    // 1 - Flower
    [SerializeField] TextMeshProUGUI[] attackSpeedUI;
    [SerializeField] TextMeshProUGUI[] lifetimeUI;
    [SerializeField] TextMeshProUGUI[] rangeUI;
    [SerializeField] TextMeshProUGUI[] damageUI;
    [SerializeField] TextMeshProUGUI[] upgradeCostUI;
    [SerializeField] TextMeshProUGUI[] levelUI;
    [SerializeField] Image[] levelFillUI;


    private void OnGUI()
    {
        currencyUI.text = LevelManager.Instance.Currency.ToString();

        // Cannon (Type Turret)
        attackSpeedUI[0].text = Lobspike.bps.ToString("F2");
        lifetimeUI[0].text = Lobspike.lifetime.ToString();
        rangeUI[0].text = Lobspike.targetingRange.ToString("F2");
        damageUI[0].text = Lobspike.damage.ToString();
        upgradeCostUI[0].text = Lobspike.CalculateCost().ToString();
        levelUI[0].text = Lobspike.level.ToString();
        levelFillUI[0].fillAmount = Lobspike.level / (float)Lobspike.maxLevel;

        if(Lobspike.CalculateCost() > LevelManager.Instance.Currency)
        {
            upgradeCostUI[0].color = Color.red;
        }
        else
        {
            upgradeCostUI[0].color = Color.black;
        }

        // Flower (Type Turret)
        attackSpeedUI[1].text = Flower.bps.ToString("F2");
        lifetimeUI[1].text = Flower.lifetime.ToString();
        rangeUI[1].text = Flower.rotationSpeed.ToString();
        damageUI[1].text = Flower.damage.ToString();
        upgradeCostUI[1].text = Flower.CalculateCost().ToString();
        levelUI[1].text = Flower.level.ToString();
        levelFillUI[1].fillAmount = Flower.level / (float)Flower.maxLevel;

        if (Lobspike.CalculateCost() > LevelManager.Instance.Currency)
        {
            upgradeCostUI[1].color = Color.red;
        }
        else
        {
            upgradeCostUI[1].color = Color.black;
        }

        // Cannon (Type Turret)
        attackSpeedUI[2].text = Seedsnap.bps.ToString("F2");
        lifetimeUI[2].text = Seedsnap.lifetime.ToString();
        rangeUI[2].text = Seedsnap.targetingRange.ToString("F2");
        damageUI[2].text = Seedsnap.damage.ToString();
        upgradeCostUI[2].text = Seedsnap.CalculateCost().ToString();
        levelUI[2].text = Seedsnap.level.ToString();
        levelFillUI[2].fillAmount = Seedsnap.level / (float)Seedsnap.maxLevel;

        if (Seedsnap.CalculateCost() > LevelManager.Instance.Currency)
        {
            upgradeCostUI[2].color = Color.red;
        }
        else
        {
            upgradeCostUI[2].color = Color.black;
        }

    }

}
