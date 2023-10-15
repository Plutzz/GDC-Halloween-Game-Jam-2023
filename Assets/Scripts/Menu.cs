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
        attackSpeedUI[0].text = Cannon.bps.ToString("F2");
        lifetimeUI[0].text = Cannon.lifetime.ToString() + "s";
        rangeUI[0].text = Cannon.targetingRange.ToString("F2");
        damageUI[0].text = Cannon.damage.ToString();
        upgradeCostUI[0].text = Cannon.CalculateCost().ToString();
        levelUI[0].text = Cannon.level.ToString();
        levelFillUI[0].fillAmount = Cannon.level / 5f;
        
    }

}
