using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class Seedsnap : BaseTurret
{
    protected override void Start()
    {
        base.Start();

        //SET BASE CANNON VALUES
        //---------------------------

        // Cost
        baseUpgradeCost = 1000;
        upgradeCostFactor = 0.8f;

        // Bullets Per Second
        bpsBase = 1f;
        bpsUpgradeFactor = 0.6f;

        // Damage
        damageBase = 5;
        damageUpgradeFactor = 1;

        // Range
        targetingRangeBase = 5f;
        targetingRangeUpgradeFactor = 0.6f;

        // Other Varibles
        lifetime = 20;
        rotationSpeed = 500;
    }
    protected static new void CalculateAttributes()
    {
        Debug.Log("NEW UPGRADE CALL");
    }
}


